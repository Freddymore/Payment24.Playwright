using Microsoft.Playwright;
using Payment24.Playwright.Framework.Configuration;

namespace Payment24.Playwright.Framework.Core;

public class BrowserManager
{
    public IPlaywright Playwright { get; private set; } = null!;
    public IBrowser Browser { get; private set; } = null!;
    public IBrowserContext Context { get; private set; } = null!;
    public IPage Page { get; private set; } = null!;

    public async Task StartBrowserAsync()
    {
        var browserSettings = ConfigManager.Browser;

        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Browser = await Playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Channel = browserSettings.Channel,
                Headless = browserSettings.Headless,
                SlowMo = browserSettings.SlowMo,
                Args = browserSettings.StartMaximized
                    ? new[] { "--start-maximized" }
                    : null
            });

        Context = await Browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                ViewportSize = browserSettings.StartMaximized
                    ? null
                    : new ViewportSize
                    {
                        Width = 1920,
                        Height = 1080
                    }
            });

        Page = await Context.NewPageAsync();
    }

    public async Task CloseBrowserAsync()
    {
        if (Context != null)
            await Context.CloseAsync();

        if (Browser != null)
            await Browser.CloseAsync();

        Playwright?.Dispose();
    }
}