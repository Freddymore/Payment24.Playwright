using Microsoft.Extensions.Configuration;

namespace Payment24.Playwright.Framework.Configuration;

public static class ConfigManager
{
    public static IConfiguration Configuration { get; }

    public static BrowserSettings Browser =>
        Configuration.GetSection("Browser").Get<BrowserSettings>()!;

    public static PortalSettings Portal =>
        Configuration.GetSection("Portal").Get<PortalSettings>()!;

    static ConfigManager()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();
    }
}