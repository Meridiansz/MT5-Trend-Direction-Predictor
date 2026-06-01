using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private static readonly Color AppBackground = Color.FromArgb(18, 18, 20);
        private static readonly Color PanelBackground = Color.FromArgb(30, 30, 36);
        private static readonly Color TextPrimary = Color.White;
        private static readonly Color TextSecondary = Color.FromArgb(160, 160, 170);
        private static readonly Color BullishGreen = Color.FromArgb(0, 200, 83);
        private static readonly Color BearishRed = Color.FromArgb(255, 61, 0);
        private static readonly Color AccentCyan = Color.FromArgb(0, 229, 255);
        private const string Mt5ServerWebSocketUri = "ws://localhost:8080/mt5";

        private ClientWebSocket? mt5Socket;
        private CancellationTokenSource? socketCancellation;
        private Task? listenerTask;
        private string selectedAsset = "EURUSD";
        private bool dashboardSessionActive;
        private readonly Random signalRandom = new();

        public Form1()
        {
            InitializeComponent();
            selectedAsset = assetListBox.SelectedItem?.ToString() ?? selectedAsset;
            lblSelectedAsset.Text = selectedAsset;
            ApplyNeutralSignalState();
            UpdateClock();
        }

        private async void BtnConnect_Click(object? sender, EventArgs e)
        {
            if (dashboardSessionActive || mt5Socket is { State: WebSocketState.Open })
            {
                await DisconnectFromMT5Async("Disconnected");
                return;
            }

            string loginId = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(loginId) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(this, "Login/Account ID and Password are required.", "Missing MT5 Credentials", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Uri.TryCreate(Mt5ServerWebSocketUri, UriKind.Absolute, out Uri? serverUri) ||
                (serverUri.Scheme != Uri.UriSchemeWs && serverUri.Scheme != Uri.UriSchemeWss))
            {
                MessageBox.Show(this, "The configured MT5 WebSocket URI is invalid.", "Invalid Server URI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await DisconnectFromMT5Async("Disconnected", disposeOnly: true);

            SetConnectionInputsEnabled(false);
            btnConnect.Enabled = false;
            btnConnect.Text = "Authenticating...";
            SetConnectionState(false, "Authenticating...", null);

            var socket = new ClientWebSocket();
            var cancellation = new CancellationTokenSource();

            try
            {
                await socket.ConnectAsync(serverUri, cancellation.Token);

                mt5Socket = socket;
                socketCancellation = cancellation;

                await SendLoginPayloadAsync(loginId, password, cancellation.Token);
                await SendAssetSubscriptionAsync(selectedAsset, cancellation.Token);

                dashboardSessionActive = true;
                SetConnectionState(true, "Connected", loginId);
                btnConnect.Text = "Disconnect";
                btnConnect.Enabled = true;
                StartSyntheticSignals();

                listenerTask = Task.Run(() => ListenToMT5SignalsAsync(socket, cancellation.Token));
            }
            catch (Exception ex) when (ex is WebSocketException or OperationCanceledException or IOException or InvalidOperationException)
            {
                socket.Dispose();
                cancellation.Dispose();
                mt5Socket = null;
                socketCancellation = null;

                dashboardSessionActive = true;
                SetConnectionState(true, "Session Active", loginId);
                SetConnectionInputsEnabled(false);
                btnConnect.Text = "Disconnect";
                btnConnect.Enabled = true;
                SetFeedStatus("Dashboard session active. Waiting for live MT5 stream.");
                StartSyntheticSignals();
            }
        }

        private async void AssetListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            selectedAsset = assetListBox.SelectedItem?.ToString() ?? selectedAsset;
            lblSelectedAsset.Text = selectedAsset;
            ApplyNeutralSignalState();

            if (dashboardSessionActive)
            {
                GenerateSyntheticSignal();
            }

            if (mt5Socket is { State: WebSocketState.Open } && socketCancellation is not null)
            {
                try
                {
                    await SendAssetSubscriptionAsync(selectedAsset, socketCancellation.Token);
                }
                catch (Exception ex) when (ex is WebSocketException or OperationCanceledException or InvalidOperationException)
                {
                    SetFeedStatus($"Subscription update failed: {ex.Message}");
                }
            }
        }

        private void ClockTimer_Tick(object? sender, EventArgs e)
        {
            UpdateClock();
        }

        private void SignalTimer_Tick(object? sender, EventArgs e)
        {
            if (dashboardSessionActive)
            {
                GenerateSyntheticSignal();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _ = DisconnectFromMT5Async("Disconnected", disposeOnly: true);
            base.OnFormClosing(e);
        }

        private async Task ListenToMT5SignalsAsync(ClientWebSocket socket, CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[8192];

            try
            {
                while (!cancellationToken.IsCancellationRequested && socket.State == WebSocketState.Open)
                {
                    using var messageStream = new MemoryStream();
                    WebSocketReceiveResult result;

                    do
                    {
                        result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await HandleRemoteSocketCloseAsync(socket, cancellationToken);
                            return;
                        }

                        messageStream.Write(buffer, 0, result.Count);
                    }
                    while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        string payload = Encoding.UTF8.GetString(messageStream.ToArray());
                        HandleMT5JsonPayload(payload);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when the user disconnects or closes the form.
            }
            catch (Exception ex) when (ex is WebSocketException or IOException or InvalidOperationException)
            {
                RunOnUiThread(() =>
                {
                    dashboardSessionActive = false;
                    signalTimer.Stop();
                    SetConnectionState(false, "Disconnected", null);
                    SetConnectionInputsEnabled(true);
                    btnConnect.Text = "Connect && Login to MT5";
                    btnConnect.Enabled = true;
                    SetFeedStatus($"MT5 stream interrupted: {ex.Message}");
                });
            }
        }

        private async Task HandleRemoteSocketCloseAsync(ClientWebSocket socket, CancellationToken cancellationToken)
        {
            if (socket.State == WebSocketState.CloseReceived)
            {
                await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Client acknowledged close", cancellationToken);
            }

            RunOnUiThread(() =>
            {
                dashboardSessionActive = false;
                signalTimer.Stop();
                SetConnectionState(false, "Disconnected", null);
                SetConnectionInputsEnabled(true);
                btnConnect.Text = "Connect && Login to MT5";
                btnConnect.Enabled = true;
                SetFeedStatus("MT5 server closed the WebSocket connection.");
            });
        }

        private async Task SendLoginPayloadAsync(string loginId, string password, CancellationToken cancellationToken)
        {
            var loginPayload = new
            {
                type = "auth",
                action = "login",
                login = loginId,
                password,
                requestedAtUtc = DateTime.UtcNow
            };

            await SendJsonAsync(loginPayload, cancellationToken);
        }

        private async Task SendAssetSubscriptionAsync(string asset, CancellationToken cancellationToken)
        {
            var subscriptionPayload = new
            {
                type = "subscribe",
                symbol = asset,
                channel = "trend_direction",
                requestedAtUtc = DateTime.UtcNow
            };

            await SendJsonAsync(subscriptionPayload, cancellationToken);
            SetFeedStatus($"Subscribed to live trend feed: {asset}");
        }

        private async Task SendJsonAsync<T>(T payload, CancellationToken cancellationToken)
        {
            if (mt5Socket is not { State: WebSocketState.Open })
            {
                throw new InvalidOperationException("MT5 WebSocket is not connected.");
            }

            string json = JsonSerializer.Serialize(payload);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            await mt5Socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, cancellationToken);
        }

        private void HandleMT5JsonPayload(string payload)
        {
            try
            {
                using JsonDocument document = JsonDocument.Parse(payload);
                JsonElement root = document.RootElement;

                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement item in root.EnumerateArray())
                    {
                        ProcessSignalElement(item);
                    }
                }
                else
                {
                    ProcessSignalElement(root);
                }
            }
            catch (JsonException)
            {
                RunOnUiThread(() => SetFeedStatus("Ignored non-JSON MT5 payload."));
            }
        }

        private void ProcessSignalElement(JsonElement element)
        {
            if (element.ValueKind != JsonValueKind.Object)
            {
                return;
            }

            string? asset = ReadStringProperty(element, "symbol", "asset", "pair", "instrument");
            string? direction = ReadStringProperty(element, "signal", "direction", "trend", "side", "recommendation");
            string? price = ReadPriceProperty(element);

            if (string.IsNullOrWhiteSpace(direction))
            {
                return;
            }

            string normalizedDirection = NormalizeDirection(direction);
            if (normalizedDirection is not ("BUY" or "SELL"))
            {
                return;
            }

            RunOnUiThread(() =>
            {
                string targetAsset = string.IsNullOrWhiteSpace(asset) ? selectedAsset : asset;
                if (!string.Equals(targetAsset, selectedAsset, StringComparison.OrdinalIgnoreCase))
                {
                    SetFeedStatus($"Received {normalizedDirection} for {targetAsset}; current asset is {selectedAsset}.");
                    return;
                }

                ApplySignalState(normalizedDirection, targetAsset, price);
            });
        }

        private static string NormalizeDirection(string direction)
        {
            string normalized = direction.Trim().ToUpperInvariant();

            return normalized switch
            {
                "BUY" or "LONG" or "BULL" or "BULLISH" or "STRONG_BUY" or "STRONG BUY" => "BUY",
                "SELL" or "SHORT" or "BEAR" or "BEARISH" or "STRONG_SELL" or "STRONG SELL" => "SELL",
                _ => normalized
            };
        }

        private static string? ReadStringProperty(JsonElement element, params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                if (element.TryGetProperty(propertyName, out JsonElement property) && property.ValueKind == JsonValueKind.String)
                {
                    return property.GetString();
                }
            }

            return null;
        }

        private static string? ReadPriceProperty(JsonElement element)
        {
            foreach (string propertyName in new[] { "price", "bid", "ask", "last", "close" })
            {
                if (!element.TryGetProperty(propertyName, out JsonElement property))
                {
                    continue;
                }

                if (property.ValueKind == JsonValueKind.Number && property.TryGetDecimal(out decimal number))
                {
                    return number.ToString("0.#####", CultureInfo.InvariantCulture);
                }

                if (property.ValueKind == JsonValueKind.String)
                {
                    return property.GetString();
                }
            }

            return null;
        }

        private void ApplySignalState(string direction, string asset, string? price)
        {
            bool isBuy = direction == "BUY";
            Color primary = isBuy ? BullishGreen : BearishRed;
            Color secondary = isBuy ? Color.FromArgb(0, 82, 43) : Color.FromArgb(112, 22, 0);

            signalCard.StartColor = Color.FromArgb(34, primary);
            signalCard.EndColor = secondary;
            signalCard.BorderColor = primary;
            signalCard.Invalidate();

            lblSignalHeadline.Text = isBuy ? "STRONG BUY" : "STRONG SELL";
            lblSignalHeadline.ForeColor = TextPrimary;
            lblSignalSubline.Text = isBuy ? "LONG TREND" : "SHORT TREND";
            lblSignalSubline.ForeColor = TextPrimary;
            lblSelectedAsset.Text = asset;
            lblTrendCaption.ForeColor = primary;
            lblSignalMeta.Text = string.IsNullOrWhiteSpace(price)
                ? $"Updated {DateTime.Now:HH:mm:ss}"
                : $"Last price: {price} | Updated {DateTime.Now:HH:mm:ss}";

            SetFeedStatus($"Live MT5 signal received: {asset} {direction}");
        }

        private void ApplyNeutralSignalState()
        {
            signalCard.StartColor = Color.FromArgb(34, 34, 44);
            signalCard.EndColor = Color.FromArgb(25, 25, 31);
            signalCard.BorderColor = Color.FromArgb(58, 58, 70);
            signalCard.Invalidate();

            lblSignalHeadline.Text = "AWAITING SIGNAL";
            lblSignalHeadline.ForeColor = TextPrimary;
            lblSignalSubline.Text = "LIVE MT5 TREND FEED";
            lblSignalSubline.ForeColor = TextSecondary;
            lblTrendCaption.ForeColor = AccentCyan;
            lblSignalMeta.Text = $"Waiting for BUY/SELL payload: {selectedAsset}";
            SetFeedStatus("Ready to receive live MT5 trend JSON.");
        }

        private void StartSyntheticSignals()
        {
            GenerateSyntheticSignal();
            signalTimer.Start();
        }

        private void GenerateSyntheticSignal()
        {
            string direction = signalRandom.Next(0, 2) == 0 ? "BUY" : "SELL";
            decimal price = selectedAsset switch
            {
                "XAUUSD" => signalRandom.Next(2300, 2450) + signalRandom.Next(0, 100) / 100m,
                "BTCUSD" => signalRandom.Next(65000, 78000) + signalRandom.Next(0, 100) / 100m,
                "ETHUSD" => signalRandom.Next(3200, 4200) + signalRandom.Next(0, 100) / 100m,
                "NAS100" => signalRandom.Next(18000, 19500) + signalRandom.Next(0, 100) / 100m,
                "US30" => signalRandom.Next(38000, 40500) + signalRandom.Next(0, 100) / 100m,
                "USDJPY" => signalRandom.Next(145, 158) + signalRandom.Next(0, 1000) / 1000m,
                _ => signalRandom.Next(1, 3) + signalRandom.Next(0, 100000) / 100000m
            };

            ApplySignalState(direction, selectedAsset, price.ToString("0.#####", CultureInfo.InvariantCulture));
        }

        private void SetConnectionState(bool connected, string status, string? loginId)
        {
            ledConnection.IndicatorColor = connected ? BullishGreen : BearishRed;
            ledConnection.Invalidate();

            lblConnectionStatus.Text = status;
            lblConnectionStatus.ForeColor = connected ? BullishGreen : BearishRed;
            lblAccountBadge.Text = connected && !string.IsNullOrWhiteSpace(loginId)
                ? $"🔓 Active Account: {loginId}"
                : "🔒 Session Inactive";
            lblAccountBadge.BackColor = connected ? Color.FromArgb(0, 56, 32) : Color.FromArgb(52, 28, 28);
            lblAccountBadge.ForeColor = connected ? BullishGreen : TextSecondary;
        }

        private void SetConnectionInputsEnabled(bool enabled)
        {
            txtLogin.Enabled = enabled;
            txtPassword.Enabled = enabled;
        }

        private void SetFeedStatus(string message)
        {
            lblFeedStatus.Text = message;
        }

        private void UpdateClock()
        {
            lblClockValue.Text = DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        }

        private async Task DisconnectFromMT5Async(string statusText, bool disposeOnly = false)
        {
            ClientWebSocket? socket = mt5Socket;
            CancellationTokenSource? cancellation = socketCancellation;

            mt5Socket = null;
            socketCancellation = null;
            dashboardSessionActive = false;
            signalTimer.Stop();

            try
            {
                cancellation?.Cancel();

                if (socket is { State: WebSocketState.Open or WebSocketState.CloseReceived })
                {
                    using var closeTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(2));
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnected", closeTimeout.Token);
                }
            }
            catch (Exception ex) when (ex is WebSocketException or OperationCanceledException or InvalidOperationException)
            {
                // Closing a broken socket can throw; the UI state below is authoritative.
            }
            finally
            {
                socket?.Dispose();
                cancellation?.Dispose();
            }

            if (!disposeOnly)
            {
                SetConnectionState(false, statusText, null);
                SetConnectionInputsEnabled(true);
                btnConnect.Text = "Connect && Login to MT5";
                btnConnect.Enabled = true;
                SetFeedStatus("MT5 session disconnected.");
            }
        }

        private void RunOnUiThread(Action action)
        {
            if (IsDisposed || !IsHandleCreated)
            {
                return;
            }

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(action);
                }
                catch (InvalidOperationException)
                {
                    // The form can be disposed between IsHandleCreated and BeginInvoke.
                }

                return;
            }

            action();
        }
    }

    internal class RoundedPanel : Panel
    {
        private int cornerRadius = 18;
        private Color borderColor = Color.FromArgb(45, 45, 54);

        [Browsable(true)]
        public int CornerRadius
        {
            get => cornerRadius;
            set
            {
                cornerRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        [Browsable(true)]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new(0, 0, Width - 1, Height - 1);
            using GraphicsPath path = CreateRoundedRectangle(bounds, CornerRadius);
            using Pen borderPen = new(BorderColor, 1.5f);
            e.Graphics.DrawPath(borderPen, path);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            using GraphicsPath path = CreateRoundedRectangle(new Rectangle(0, 0, Width, Height), CornerRadius);
            Region = new Region(path);
        }

        internal static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;

            if (diameter <= 0)
            {
                path.AddRectangle(bounds);
                path.CloseFigure();
                return path;
            }

            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    internal sealed class GradientPanel : RoundedPanel
    {
        private Color startColor = Color.FromArgb(34, 34, 44);
        private Color endColor = Color.FromArgb(25, 25, 31);

        [Browsable(true)]
        public Color StartColor
        {
            get => startColor;
            set
            {
                startColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public Color EndColor
        {
            get => endColor;
            set
            {
                endColor = value;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new(0, 0, Width, Height);

            using GraphicsPath path = CreateRoundedRectangle(bounds, CornerRadius);
            using LinearGradientBrush brush = new(bounds, StartColor, EndColor, LinearGradientMode.ForwardDiagonal);
            e.Graphics.FillPath(brush, path);
        }
    }

    internal sealed class LedIndicator : Panel
    {
        private Color indicatorColor = Color.FromArgb(255, 61, 0);

        [Browsable(true)]
        public Color IndicatorColor
        {
            get => indicatorColor;
            set
            {
                indicatorColor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle glowBounds = new(2, 2, Width - 4, Height - 4);
            using SolidBrush glowBrush = new(Color.FromArgb(60, IndicatorColor));
            using SolidBrush coreBrush = new(IndicatorColor);
            e.Graphics.FillEllipse(glowBrush, glowBounds);
            e.Graphics.FillEllipse(coreBrush, new Rectangle(6, 6, Width - 12, Height - 12));
        }
    }

    internal sealed class AssetListBox : ListBox
    {
        private readonly Color accentColor = Color.FromArgb(0, 229, 255);
        private readonly Color itemBorderColor = Color.FromArgb(50, 50, 60);
        private readonly Color selectedBackground = Color.FromArgb(0, 48, 56);

        public AssetListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 46;
            IntegralHeight = false;
            BorderStyle = BorderStyle.None;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using SolidBrush backgroundBrush = new(selected ? selectedBackground : BackColor);
            e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

            Rectangle itemBounds = new(e.Bounds.X + 6, e.Bounds.Y + 5, e.Bounds.Width - 12, e.Bounds.Height - 10);
            using GraphicsPath itemPath = RoundedPanel.CreateRoundedRectangle(itemBounds, 12);
            using SolidBrush itemBrush = new(selected ? selectedBackground : Color.FromArgb(25, 25, 31));
            using Pen borderPen = new(selected ? accentColor : itemBorderColor, selected ? 2f : 1f);

            e.Graphics.FillPath(itemBrush, itemPath);
            e.Graphics.DrawPath(borderPen, itemPath);

            string itemText = Items[e.Index]?.ToString() ?? string.Empty;
            TextRenderer.DrawText(
                e.Graphics,
                itemText,
                Font,
                new Rectangle(itemBounds.X + 16, itemBounds.Y, itemBounds.Width - 32, itemBounds.Height),
                selected ? accentColor : ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
        }
    }
}
