<div align="center">

Topics: metatrader5, mql5, forex-signals, expert-advisor, mql4, metatrader, forex-trading, indicators, technical-analysis, trading-signals, trend-following, buy-sell-signals, trend-analysis, mt4, mt5, trading-bot, mt5-trend-predictor, live-forex-signals, mt5-buy-sell-signals

# MT5 Live Signal Dashboard

**A sharp desktop signal monitor for MetaTrader 5 style trading streams. The app focuses on instant BUY / SELL visibility, account connection status, selected asset tracking, and a clean dark UI that makes live market direction easy to read at a glance.**

<br>

[![Stars](https://img.shields.io/badge/Stars-Repository-00D4AA?style=for-the-badge)](https://github.com/your-username/volume-profile-mt5/stargazers)
[![Forks](https://img.shields.io/badge/Forks-Community-4D9FFF?style=for-the-badge)](https://github.com/your-username/volume-profile-mt5/network)
[![Issues](https://img.shields.io/badge/Issues-Tracker-FF4D6A?style=for-the-badge)](https://github.com/your-username/volume-profile-mt5/issues)
[![Platform](https://img.shields.io/badge/Platform-MetaTrader%205-00D4AA?style=for-the-badge)](https://www.metatrader5.com)
[![License](https://img.shields.io/badge/License-MIT-4D9FFF?style=for-the-badge)](LICENSE)
</div>

---

## Screenshot
<img width="1184" height="764" alt="Screenshot_1" src="https://github.com/user-attachments/assets/5be9f211-5797-453e-bee6-0ca0bef28b51" />

---

## 🎬 Demo

## 🎬 Demo

<div align="center">

<img src="https://i.imgur.com/vcYU7D1.gif" alt="Demo">

</div>

---

## Why This Project

Most trading tools show too much information when the trader only needs the next decision. **MT5 Live Signal Dashboard** keeps the interface direct: connect an account, choose an asset, and watch the current signal flip between long and short conditions.

It is built for:

- Forex signal dashboard presentations
- MT5 trend feed experiments
- Trading UI portfolio projects
- Local WebSocket bridge testing
- BUY / SELL signal visualization

---

## What It Does

| Module | Description |
|---|---|
| Account Panel | Collects login/account ID and MT5 password |
| WebSocket Client | Connects to `ws://localhost:8080/mt5` when a bridge is running |
| Asset Selector | Lets the operator choose a symbol from a custom dark list |
| Signal Card | Displays STRONG BUY, STRONG SELL, or AWAITING SIGNAL |
| Live Feed Handler | Parses JSON trend payloads from MT5 bridge services |
| Synthetic Mode | Generates demo signals when the live stream is unavailable |
| Status Footer | Shows account session state and local clock |

---

## Feature Highlights

| Feature | Detail |
|---|---|
| High Contrast Signal Card | Large directional label with green/red state changes |
| MT5 Login Workflow | Auth payload is sent before symbol subscription |
| Asset-Specific Signals | Ignores signals for symbols that are not currently selected |
| JSON Signal Parser | Reads `symbol`, `signal`, `direction`, `trend`, `side`, and price fields |
| Rounded Panels | Custom WinForms controls create a polished desktop look |
| LED Connection Indicator | Immediate visual feedback for connection status |
| Demo Signal Timer | Keeps the app visually active for screenshots and videos |

---

## Signal Logic

```text
Incoming payload
   |
   v
Extract symbol + direction + price
   |
   v
Normalize direction
   |
   +-- BUY, LONG, BULLISH      -> STRONG BUY
   |
   +-- SELL, SHORT, BEARISH    -> STRONG SELL
   |
   +-- Unknown                 -> Ignore
```

---

## MT5 WebSocket Feed

Example payload:

```json
{
  "symbol": "XAUUSD",
  "direction": "BUY",
  "price": 2350.42
}
```

Supported direction aliases include:

```text
BUY:  BUY, LONG, BULL, BULLISH, STRONG_BUY, STRONG BUY
SELL: SELL, SHORT, BEAR, BEARISH, STRONG_SELL, STRONG SELL
```

---

## Quick Start

**Requirements:**

- Windows 10 or Windows 11
- .NET 8 SDK
- Visual Studio 2022

```bash
git clone https://github.com/your-username/mt5-live-signal-dashboard.git
cd mt5-live-signal-dashboard
```

Open `WinFormsApp2.slnx` in Visual Studio and press **F5**.

---

## How to Use

1. Start the application.
2. Enter login/account ID and password.
3. Click **Connect && Login to MT5**.
4. Select an asset from the left panel.
5. Send live JSON signals from your MT5 bridge or use the synthetic demo mode.
6. Watch the signal card update instantly.

---

## Roadmap

- [x] BUY / SELL signal renderer
- [x] MT5 WebSocket login payload
- [x] Asset subscription payload
- [x] Synthetic signal fallback
- [ ] Add signal history table
- [ ] Add alert sound profiles
- [ ] Add confidence scoring
- [ ] Add broker bridge setup guide

---

## License

MIT

---

<div align="center">

MT5 Live Signal Dashboard - Fast BUY / SELL Signal Monitor

</div>
