using Microsoft.Playwright;
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

    #region Navigation

    protected async Task NavigateToAsync(string url)
    {
        await Page.GotoAsync(url);
        await Wait.WaitForPageLoadAsync();
    }

    protected async Task RefreshPageAsync()
    {
        await Page.ReloadAsync();
        await Wait.WaitForPageLoadAsync();
    }

    #endregion

    #region Element Actions

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
        await Page.Locator(selector).ClearAsync();
        await Page.Locator(selector).FillAsync(text);
    }

    protected async Task SelectDropdownAsync(string selector, string value)
    {
        await Page.Locator(selector).SelectOptionAsync(value);
    }

    protected async Task CheckCheckboxAsync(string selector)
    {
        await Page.Locator(selector).CheckAsync();
    }

    protected async Task UncheckCheckboxAsync(string selector)
    {
        await Page.Locator(selector).UncheckAsync();
    }

    #endregion

    #region Get Information

    protected async Task<string> GetTextAsync(string selector)
    {
        return await Page.Locator(selector).InnerTextAsync();
    }

    protected async Task<string?> GetValueAsync(string selector)
    {
        return await Page.Locator(selector).InputValueAsync();
    }

    protected async Task<bool> IsVisibleAsync(string selector)
    {
        return await Page.Locator(selector).IsVisibleAsync();
    }

    protected async Task<bool> IsEnabledAsync(string selector)
    {
        return await Page.Locator(selector).IsEnabledAsync();
    }

    #endregion

    #region Waits

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

    #endregion

    #region Utilities

    protected async Task ScrollIntoViewAsync(string selector)
    {
        await Page.Locator(selector).ScrollIntoViewIfNeededAsync();
    }

    protected async Task PressKeyAsync(string selector, string key)
    {
        await Page.Locator(selector).PressAsync(key);
    }

    protected async Task HoverAsync(string selector)
    {
        await Page.Locator(selector).HoverAsync();
    }

    #endregion

    #region Screenshots

    protected async Task TakeScreenshotAsync(string fileName)
    {
        Directory.CreateDirectory("Screenshots");

        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = Path.Combine("Screenshots", $"{fileName}.png"),
            FullPage = true
        });
    }

    #endregion
}