using Microsoft.Playwright;
using Payment24.Playwright.Framework.Configuration;
using Payment24.Playwright.Framework.Helpers;

namespace Payment24.Playwright.Framework.Pages;

public abstract class BasePage
{
    protected readonly IPage Page;
    protected readonly WaitHelper Wait;

    protected BasePage(IPage page)
    {
        Page = page;
        Wait = new WaitHelper(page);
    }

    // =====================================================
    // Navigation
    // =====================================================

    protected async Task NavigateToAsync(string url)
    {
        if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            url = $"{ConfigManager.Portal.BaseUrl}{url}";
        }

        await Page.GotoAsync(url);

        await WaitForPageLoadAsync();
    }

    // =====================================================
    // Element Actions
    // =====================================================

    protected async Task ClickAsync(string selector)
    {
        await Page.Locator(selector).ClickAsync();
    }

    protected async Task FillAsync(string selector, string text)
    {
        await Page.Locator(selector).FillAsync(text);
    }

    protected async Task ClearAndFillAsync(string selector, string text)
    {
        var locator = Page.Locator(selector);

        await locator.ClearAsync();
        await locator.FillAsync(text);
    }

    /// <summary>
    /// Selects a dropdown option using its visible text.
    /// </summary>
    protected async Task SelectByLabelAsync(string selector, string label)
    {
        await Page.Locator(selector).SelectOptionAsync(
            new SelectOptionValue
            {
                Label = label
            });
    }

    protected async Task CheckCheckboxAsync(string selector)
    {
        await Page.Locator(selector).CheckAsync();
    }

    protected async Task UncheckCheckboxAsync(string selector)
    {
        await Page.Locator(selector).UncheckAsync();
    }

    // =====================================================
    // Get Information
    // =====================================================

    protected async Task<string> GetTextAsync(string selector)
    {
        return await Page.Locator(selector).InnerTextAsync();
    }

    protected async Task<string?> GetValueAsync(string selector)
    {
        return await Page.Locator(selector).InputValueAsync();
    }

    protected async Task<string?> GetAttributeAsync(string selector, string attribute)
    {
        return await Page.Locator(selector).GetAttributeAsync(attribute);
    }

    protected async Task<bool> IsVisibleAsync(string selector)
    {
        return await Page.Locator(selector).IsVisibleAsync();
    }

    protected async Task<bool> IsEnabledAsync(string selector)
    {
        return await Page.Locator(selector).IsEnabledAsync();
    }

    protected async Task<bool> IsCheckedAsync(string selector)
    {
        return await Page.Locator(selector).IsCheckedAsync();
    }

    protected async Task<bool> ExistsAsync(string selector)
    {
        return await Page.Locator(selector).CountAsync() > 0;
    }

    protected async Task<int> GetCountAsync(string selector)
    {
        return await Page.Locator(selector).CountAsync();
    }

    // =====================================================
    // Waits
    // =====================================================

    protected async Task WaitForElementAsync(string selector)
    {
        await Wait.WaitForElementVisibleAsync(selector);
    }

    protected async Task WaitForElementToDisappearAsync(string selector)
    {
        await Wait.WaitForElementHiddenAsync(selector);
    }

    protected async Task WaitForPageLoadAsync()
    {
        await Wait.WaitForPageLoadAsync();
    }

    protected async Task WaitForNetworkIdleAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    // =====================================================
    // Utilities
    // =====================================================

    protected async Task ScrollIntoViewAsync(string selector)
    {
        await Page.Locator(selector).ScrollIntoViewIfNeededAsync();
    }

    protected async Task HoverAsync(string selector)
    {
        await Page.Locator(selector).HoverAsync();
    }

    protected async Task PressKeyAsync(string selector, string key)
    {
        await Page.Locator(selector).PressAsync(key);
    }

    protected async Task FocusAsync(string selector)
    {
        await Page.Locator(selector).FocusAsync();
    }

    // =====================================================
    // Screenshots
    // =====================================================

    protected async Task TakeScreenshotAsync(string fileName)
    {
        Directory.CreateDirectory("Screenshots");

        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = Path.Combine("Screenshots", $"{fileName}.png"),
            FullPage = true
        });
    }
}