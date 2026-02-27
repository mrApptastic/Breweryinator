# Breweryinator 🍺

Storage management for home-brewed beer. Track your recipes, batches and their status from brew to bottle.

## Projects

| Project | Description |
|---------|-------------|
| `Breweryinator.Shared` | Shared models and DTOs used by both API and Web |
| `Breweryinator.Api` | ASP.NET Core WebAPI with EF Core, Google Auth, SQLite (dev) / MySQL (prod) |
| `Breweryinator.Web` | Blazor WebAssembly PWA with Google login and Bootstrap theme |
| `Breweryinator.Theme` | Node.js app that compiles the custom Bootstrap 5 theme + Bootstrap Icons |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- A [Google Cloud Console](https://console.cloud.google.com/) project with an OAuth 2.0 Client ID

## Getting Started

### 1. Build the Bootstrap theme

```bash
cd src/Breweryinator.Theme
npm install
npm run build
```

This compiles the custom theme and copies it to `Breweryinator.Web/wwwroot/css/`.

### 2. Configure Google OAuth

In **both** `src/Breweryinator.Api/appsettings.Development.json` and `src/Breweryinator.Web/wwwroot/appsettings.Development.json`, replace `YOUR_GOOGLE_CLIENT_ID` with the Client ID from your Google Cloud project.

> **Tip:** Use `appsettings.Local.json` (git-ignored) for local secrets.

Add `http://localhost:5173` (Web) and `https://localhost:7001` (API) as authorised origins / redirect URIs in your Google Cloud Console.

### 3. Run the API

```bash
cd src/Breweryinator.Api
dotnet run
```

The API starts on `https://localhost:7001`. On first run it creates the SQLite database (`breweryinator.db`) and seeds sample data.

### 4. Run the Blazor WASM app

```bash
cd src/Breweryinator.Web
dotnet run
```

Open `http://localhost:5173` in your browser.

## Production Deployment

### API (MySQL)

Set the `ConnectionStrings__DefaultConnection` environment variable to your MySQL connection string. The app uses **MySQL** automatically when not in the `Development` environment.

### Blazor WASM (GitHub Pages)

Push to `main` / `master` — the **Deploy to GitHub Pages** workflow publishes the Blazor WASM app automatically.

Before deploying, enable GitHub Pages in your repository settings (Source: GitHub Actions) and optionally set the `GITHUB_PAGES_BASE` repository variable to your sub-path (e.g. `/Breweryinator/`).

## Architecture

```
┌─────────────────────────┐      OIDC / JWT       ┌──────────────────────────┐
│  Blazor WASM (PWA)      │ ──────────────────────▶│  Google Accounts         │
│  Bootstrap 5 (custom)   │                        └──────────────────────────┘
│  GitHub Pages           │
└───────────┬─────────────┘
            │  REST + Bearer token
            ▼
┌─────────────────────────┐
│  ASP.NET Core WebAPI    │
│  EF Core                │
│  SQLite (dev)           │
│  MySQL (prod)           │
└─────────────────────────┘
```

## PWA Auto-update

When a new version is deployed, the service worker detects the change and displays a banner at the bottom of the screen prompting users to reload.
