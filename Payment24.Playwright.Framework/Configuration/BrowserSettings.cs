namespace Payment24.Playwright.Framework.Configuration;

public class BrowserSettings
{
    public string Channel { get; set; } = "chrome";

    public bool Headless { get; set; }

    public bool StartMaximized { get; set; }

    public int SlowMo { get; set; }
}