using Microsoft.Playwright;

namespace Payment24.Playwright.Framework.Pages.Components;

public class CookieBannerComponent
{
    private readonly IPage Page;

    private const string AcceptCookiesButton =
        "button.btn.btn-primary.btn-sm.acceptcookies";

    public CookieBannerComponent(IPage page)
    {
        Page = page;
    }

    public async Task AcceptIfDisplayedAsync()
    {
        var button = Page.Locator(AcceptCookiesButton);

        if (await button.IsVisibleAsync())
        {
            await button.ClickAsync();

            Console.WriteLine("✔ Cookies accepted.");
        }
        else
        {
            Console.WriteLine("✔ Cookie banner not displayed.");
        }
    }
}