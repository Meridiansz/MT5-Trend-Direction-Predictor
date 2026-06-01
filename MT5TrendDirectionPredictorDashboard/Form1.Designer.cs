namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlConnection = new RoundedPanel();
            lblConnectionTitle = new Label();
            lblConnectionSubtitle = new Label();
            lblLogin = new Label();
            txtLogin = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnConnect = new Button();
            ledConnection = new LedIndicator();
            lblConnectionStatus = new Label();
            pnlAssets = new RoundedPanel();
            lblAssetsTitle = new Label();
            lblAssetsSubtitle = new Label();
            assetListBox = new AssetListBox();
            pnlAnalytics = new RoundedPanel();
            lblAnalyticsTitle = new Label();
            lblTrendCaption = new Label();
            lblSelectedAsset = new Label();
            signalCard = new GradientPanel();
            lblSignalHeadline = new Label();
            lblSignalSubline = new Label();
            lblSignalMeta = new Label();
            lblFeedStatus = new Label();
            pnlFooter = new RoundedPanel();
            lblClockTitle = new Label();
            lblClockValue = new Label();
            lblAccountBadge = new Label();
            lblAppTitle = new Label();
            lblAppSubtitle = new Label();
            clockTimer = new System.Windows.Forms.Timer(components);
            signalTimer = new System.Windows.Forms.Timer(components);
            pnlConnection.SuspendLayout();
            pnlAssets.SuspendLayout();
            pnlAnalytics.SuspendLayout();
            signalCard.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlConnection
            // 
            pnlConnection.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pnlConnection.BackColor = Color.FromArgb(30, 30, 36);
            pnlConnection.BorderColor = Color.FromArgb(52, 52, 62);
            pnlConnection.Controls.Add(lblConnectionTitle);
            pnlConnection.Controls.Add(lblConnectionSubtitle);
            pnlConnection.Controls.Add(lblLogin);
            pnlConnection.Controls.Add(txtLogin);
            pnlConnection.Controls.Add(lblPassword);
            pnlConnection.Controls.Add(txtPassword);
            pnlConnection.Controls.Add(btnConnect);
            pnlConnection.Controls.Add(ledConnection);
            pnlConnection.Controls.Add(lblConnectionStatus);
            pnlConnection.CornerRadius = 20;
            pnlConnection.Location = new Point(24, 86);
            pnlConnection.Name = "pnlConnection";
            pnlConnection.Padding = new Padding(22);
            pnlConnection.Size = new Size(420, 196);
            pnlConnection.TabIndex = 0;
            // 
            // lblConnectionTitle
            // 
            lblConnectionTitle.AutoSize = true;
            lblConnectionTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblConnectionTitle.ForeColor = Color.White;
            lblConnectionTitle.Location = new Point(24, 20);
            lblConnectionTitle.Name = "lblConnectionTitle";
            lblConnectionTitle.Size = new Size(249, 25);
            lblConnectionTitle.TabIndex = 0;
            lblConnectionTitle.Text = "MT5 Live Account Connection";
            // 
            // lblConnectionSubtitle
            // 
            lblConnectionSubtitle.AutoSize = true;
            lblConnectionSubtitle.Font = new Font("Segoe UI", 9F);
            lblConnectionSubtitle.ForeColor = Color.FromArgb(160, 160, 170);
            lblConnectionSubtitle.Location = new Point(25, 48);
            lblConnectionSubtitle.Name = "lblConnectionSubtitle";
            lblConnectionSubtitle.Size = new Size(231, 15);
            lblConnectionSubtitle.TabIndex = 1;
            lblConnectionSubtitle.Text = "Secure WebSocket login to your MT5 bridge";
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblLogin.ForeColor = Color.FromArgb(160, 160, 170);
            lblLogin.Location = new Point(25, 78);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(103, 15);
            lblLogin.TabIndex = 2;
            lblLogin.Text = "Login / Account ID";
            // 
            // txtLogin
            // 
            txtLogin.BackColor = Color.FromArgb(18, 18, 20);
            txtLogin.BorderStyle = BorderStyle.FixedSingle;
            txtLogin.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            txtLogin.ForeColor = Color.White;
            txtLogin.Location = new Point(25, 96);
            txtLogin.Name = "txtLogin";
            txtLogin.PlaceholderText = "73981234";
            txtLogin.Size = new Size(176, 27);
            txtLogin.TabIndex = 3;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(160, 160, 170);
            lblPassword.Location = new Point(218, 78);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(18, 18, 20);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            txtPassword.ForeColor = Color.White;
            txtPassword.Location = new Point(218, 96);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "MT5 password";
            txtPassword.Size = new Size(176, 27);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(0, 229, 255);
            btnConnect.Cursor = Cursors.Hand;
            btnConnect.FlatAppearance.BorderSize = 0;
            btnConnect.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 184, 204);
            btnConnect.FlatAppearance.MouseOverBackColor = Color.FromArgb(76, 239, 255);
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnConnect.ForeColor = Color.FromArgb(8, 18, 22);
            btnConnect.Location = new Point(25, 142);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(246, 36);
            btnConnect.TabIndex = 6;
            btnConnect.Text = "Connect && Login to MT5";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += BtnConnect_Click;
            // 
            // ledConnection
            // 
            ledConnection.BackColor = Color.Transparent;
            ledConnection.IndicatorColor = Color.FromArgb(255, 61, 0);
            ledConnection.Location = new Point(292, 146);
            ledConnection.Name = "ledConnection";
            ledConnection.Size = new Size(28, 28);
            ledConnection.TabIndex = 7;
            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblConnectionStatus.ForeColor = Color.FromArgb(255, 61, 0);
            lblConnectionStatus.Location = new Point(326, 151);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new Size(91, 19);
            lblConnectionStatus.TabIndex = 8;
            lblConnectionStatus.Text = "Disconnected";
            // 
            // pnlAssets
            // 
            pnlAssets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlAssets.BackColor = Color.FromArgb(30, 30, 36);
            pnlAssets.BorderColor = Color.FromArgb(52, 52, 62);
            pnlAssets.Controls.Add(lblAssetsTitle);
            pnlAssets.Controls.Add(lblAssetsSubtitle);
            pnlAssets.Controls.Add(assetListBox);
            pnlAssets.CornerRadius = 20;
            pnlAssets.Location = new Point(24, 304);
            pnlAssets.Name = "pnlAssets";
            pnlAssets.Padding = new Padding(22);
            pnlAssets.Size = new Size(420, 410);
            pnlAssets.TabIndex = 1;
            // 
            // lblAssetsTitle
            // 
            lblAssetsTitle.AutoSize = true;
            lblAssetsTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblAssetsTitle.ForeColor = Color.White;
            lblAssetsTitle.Location = new Point(24, 20);
            lblAssetsTitle.Name = "lblAssetsTitle";
            lblAssetsTitle.Size = new Size(143, 25);
            lblAssetsTitle.TabIndex = 0;
            lblAssetsTitle.Text = "Asset Selection";
            // 
            // lblAssetsSubtitle
            // 
            lblAssetsSubtitle.AutoSize = true;
            lblAssetsSubtitle.Font = new Font("Segoe UI", 9F);
            lblAssetsSubtitle.ForeColor = Color.FromArgb(160, 160, 170);
            lblAssetsSubtitle.Location = new Point(25, 48);
            lblAssetsSubtitle.Name = "lblAssetsSubtitle";
            lblAssetsSubtitle.Size = new Size(255, 15);
            lblAssetsSubtitle.TabIndex = 1;
            lblAssetsSubtitle.Text = "Choose the instrument for dominant trend flow";
            // 
            // assetListBox
            // 
            assetListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            assetListBox.BackColor = Color.FromArgb(30, 30, 36);
            assetListBox.BorderStyle = BorderStyle.None;
            assetListBox.Cursor = Cursors.Hand;
            assetListBox.DrawMode = DrawMode.OwnerDrawFixed;
            assetListBox.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            assetListBox.ForeColor = Color.White;
            assetListBox.FormattingEnabled = true;
            assetListBox.IntegralHeight = false;
            assetListBox.ItemHeight = 46;
            assetListBox.Items.AddRange(new object[] { "EURUSD", "XAUUSD", "BTCUSD", "GBPUSD", "USDJPY", "NAS100", "US30", "ETHUSD" });
            assetListBox.Location = new Point(25, 82);
            assetListBox.Name = "assetListBox";
            assetListBox.Size = new Size(369, 302);
            assetListBox.TabIndex = 2;
            assetListBox.SelectedIndexChanged += AssetListBox_SelectedIndexChanged;
            assetListBox.SelectedIndex = 0;
            // 
            // pnlAnalytics
            // 
            pnlAnalytics.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlAnalytics.BackColor = Color.FromArgb(30, 30, 36);
            pnlAnalytics.BorderColor = Color.FromArgb(52, 52, 62);
            pnlAnalytics.Controls.Add(lblAnalyticsTitle);
            pnlAnalytics.Controls.Add(lblTrendCaption);
            pnlAnalytics.Controls.Add(lblSelectedAsset);
            pnlAnalytics.Controls.Add(signalCard);
            pnlAnalytics.Controls.Add(lblFeedStatus);
            pnlAnalytics.CornerRadius = 24;
            pnlAnalytics.Location = new Point(468, 86);
            pnlAnalytics.Name = "pnlAnalytics";
            pnlAnalytics.Padding = new Padding(34);
            pnlAnalytics.Size = new Size(688, 506);
            pnlAnalytics.TabIndex = 2;
            // 
            // lblAnalyticsTitle
            // 
            lblAnalyticsTitle.AutoSize = true;
            lblAnalyticsTitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblAnalyticsTitle.ForeColor = Color.FromArgb(0, 229, 255);
            lblAnalyticsTitle.Location = new Point(38, 32);
            lblAnalyticsTitle.Name = "lblAnalyticsTitle";
            lblAnalyticsTitle.Size = new Size(109, 19);
            lblAnalyticsTitle.TabIndex = 0;
            lblAnalyticsTitle.Text = "CORE ANALYTICS";
            // 
            // lblTrendCaption
            // 
            lblTrendCaption.AutoSize = true;
            lblTrendCaption.Font = new Font("Segoe UI", 10F);
            lblTrendCaption.ForeColor = Color.FromArgb(0, 229, 255);
            lblTrendCaption.Location = new Point(42, 102);
            lblTrendCaption.Name = "lblTrendCaption";
            lblTrendCaption.Size = new Size(177, 19);
            lblTrendCaption.TabIndex = 2;
            lblTrendCaption.Text = "Dominant Trend Direction";
            // 
            // lblSelectedAsset
            // 
            lblSelectedAsset.AutoSize = true;
            lblSelectedAsset.Font = new Font("Segoe UI Semibold", 34F, FontStyle.Bold);
            lblSelectedAsset.ForeColor = Color.White;
            lblSelectedAsset.Location = new Point(34, 48);
            lblSelectedAsset.Name = "lblSelectedAsset";
            lblSelectedAsset.Size = new Size(188, 62);
            lblSelectedAsset.TabIndex = 1;
            lblSelectedAsset.Text = "EURUSD";
            // 
            // signalCard
            // 
            signalCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            signalCard.BackColor = Color.FromArgb(34, 34, 44);
            signalCard.BorderColor = Color.FromArgb(58, 58, 70);
            signalCard.Controls.Add(lblSignalHeadline);
            signalCard.Controls.Add(lblSignalSubline);
            signalCard.Controls.Add(lblSignalMeta);
            signalCard.CornerRadius = 26;
            signalCard.EndColor = Color.FromArgb(25, 25, 31);
            signalCard.Location = new Point(38, 147);
            signalCard.Name = "signalCard";
            signalCard.Padding = new Padding(30);
            signalCard.Size = new Size(612, 274);
            signalCard.StartColor = Color.FromArgb(34, 34, 44);
            signalCard.TabIndex = 3;
            // 
            // lblSignalHeadline
            // 
            lblSignalHeadline.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblSignalHeadline.Font = new Font("Segoe UI Semibold", 39F, FontStyle.Bold);
            lblSignalHeadline.ForeColor = Color.White;
            lblSignalHeadline.Location = new Point(24, 56);
            lblSignalHeadline.Name = "lblSignalHeadline";
            lblSignalHeadline.Size = new Size(564, 72);
            lblSignalHeadline.TabIndex = 0;
            lblSignalHeadline.Text = "AWAITING SIGNAL";
            lblSignalHeadline.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSignalSubline
            // 
            lblSignalSubline.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblSignalSubline.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblSignalSubline.ForeColor = Color.FromArgb(160, 160, 170);
            lblSignalSubline.Location = new Point(28, 134);
            lblSignalSubline.Name = "lblSignalSubline";
            lblSignalSubline.Size = new Size(556, 42);
            lblSignalSubline.TabIndex = 1;
            lblSignalSubline.Text = "LIVE MT5 TREND FEED";
            lblSignalSubline.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSignalMeta
            // 
            lblSignalMeta.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblSignalMeta.Font = new Font("Segoe UI", 10F);
            lblSignalMeta.ForeColor = Color.FromArgb(220, 220, 226);
            lblSignalMeta.Location = new Point(30, 196);
            lblSignalMeta.Name = "lblSignalMeta";
            lblSignalMeta.Size = new Size(552, 28);
            lblSignalMeta.TabIndex = 2;
            lblSignalMeta.Text = "Waiting for BUY/SELL payload: EURUSD";
            lblSignalMeta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFeedStatus
            // 
            lblFeedStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblFeedStatus.Font = new Font("Segoe UI", 10F);
            lblFeedStatus.ForeColor = Color.FromArgb(160, 160, 170);
            lblFeedStatus.Location = new Point(38, 442);
            lblFeedStatus.Name = "lblFeedStatus";
            lblFeedStatus.Size = new Size(612, 32);
            lblFeedStatus.TabIndex = 4;
            lblFeedStatus.Text = "Ready to receive live MT5 trend JSON.";
            lblFeedStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlFooter
            // 
            pnlFooter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlFooter.BackColor = Color.FromArgb(30, 30, 36);
            pnlFooter.BorderColor = Color.FromArgb(52, 52, 62);
            pnlFooter.Controls.Add(lblClockTitle);
            pnlFooter.Controls.Add(lblClockValue);
            pnlFooter.Controls.Add(lblAccountBadge);
            pnlFooter.CornerRadius = 20;
            pnlFooter.Location = new Point(468, 618);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(24);
            pnlFooter.Size = new Size(688, 96);
            pnlFooter.TabIndex = 3;
            // 
            // lblClockTitle
            // 
            lblClockTitle.AutoSize = true;
            lblClockTitle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblClockTitle.ForeColor = Color.FromArgb(160, 160, 170);
            lblClockTitle.Location = new Point(28, 22);
            lblClockTitle.Name = "lblClockTitle";
            lblClockTitle.Size = new Size(124, 15);
            lblClockTitle.TabIndex = 0;
            lblClockTitle.Text = "REAL-TIME TERMINAL";
            // 
            // lblClockValue
            // 
            lblClockValue.AutoSize = true;
            lblClockValue.Font = new Font("Consolas", 28F, FontStyle.Bold);
            lblClockValue.ForeColor = Color.FromArgb(0, 229, 255);
            lblClockValue.Location = new Point(24, 38);
            lblClockValue.Name = "lblClockValue";
            lblClockValue.Size = new Size(164, 45);
            lblClockValue.TabIndex = 1;
            lblClockValue.Text = "00:00:00";
            // 
            // lblAccountBadge
            // 
            lblAccountBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAccountBadge.BackColor = Color.FromArgb(52, 28, 28);
            lblAccountBadge.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblAccountBadge.ForeColor = Color.FromArgb(160, 160, 170);
            lblAccountBadge.Location = new Point(390, 32);
            lblAccountBadge.Name = "lblAccountBadge";
            lblAccountBadge.Size = new Size(258, 36);
            lblAccountBadge.TabIndex = 2;
            lblAccountBadge.Text = "🔒 Session Inactive";
            lblAccountBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAppTitle
            // 
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.White;
            lblAppTitle.Location = new Point(24, 22);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(566, 41);
            lblAppTitle.TabIndex = 4;
            lblAppTitle.Text = "MT5 Trend Direction Predictor & Dashboard";
            // 
            // lblAppSubtitle
            // 
            lblAppSubtitle.AutoSize = true;
            lblAppSubtitle.Font = new Font("Segoe UI", 10F);
            lblAppSubtitle.ForeColor = Color.FromArgb(160, 160, 170);
            lblAppSubtitle.Location = new Point(32, 63);
            lblAppSubtitle.Name = "lblAppSubtitle";
            lblAppSubtitle.Size = new Size(392, 19);
            lblAppSubtitle.TabIndex = 5;
            lblAppSubtitle.Text = "Live BUY / SELL trend analysis and MT5 account dashboard";
            // 
            // clockTimer
            // 
            clockTimer.Enabled = true;
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            // 
            // signalTimer
            // 
            signalTimer.Interval = 5000;
            signalTimer.Tick += SignalTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 20);
            ClientSize = new Size(1180, 738);
            Controls.Add(lblAppSubtitle);
            Controls.Add(lblAppTitle);
            Controls.Add(pnlFooter);
            Controls.Add(pnlAnalytics);
            Controls.Add(pnlAssets);
            Controls.Add(pnlConnection);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1120, 720);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MT5 Trend Direction Predictor & Dashboard";
            pnlConnection.ResumeLayout(false);
            pnlConnection.PerformLayout();
            pnlAssets.ResumeLayout(false);
            pnlAssets.PerformLayout();
            pnlAnalytics.ResumeLayout(false);
            pnlAnalytics.PerformLayout();
            signalCard.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RoundedPanel pnlConnection;
        private Label lblConnectionTitle;
        private Label lblConnectionSubtitle;
        private Label lblLogin;
        private TextBox txtLogin;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnConnect;
        private LedIndicator ledConnection;
        private Label lblConnectionStatus;
        private RoundedPanel pnlAssets;
        private Label lblAssetsTitle;
        private Label lblAssetsSubtitle;
        private AssetListBox assetListBox;
        private RoundedPanel pnlAnalytics;
        private Label lblAnalyticsTitle;
        private Label lblTrendCaption;
        private Label lblSelectedAsset;
        private GradientPanel signalCard;
        private Label lblSignalHeadline;
        private Label lblSignalSubline;
        private Label lblSignalMeta;
        private Label lblFeedStatus;
        private RoundedPanel pnlFooter;
        private Label lblClockTitle;
        private Label lblClockValue;
        private Label lblAccountBadge;
        private Label lblAppTitle;
        private Label lblAppSubtitle;
        private System.Windows.Forms.Timer clockTimer;
        private System.Windows.Forms.Timer signalTimer;
    }
}
