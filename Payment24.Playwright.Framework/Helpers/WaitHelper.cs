using Microsoft.Playwright;

namespace Payment24.Playwright.Framework.Helpers;

public class WaitHelper
{
    private readonly IPage _page;

    public WaitHelper(IPage page)
    {
        _page = page;
    }

    public async Task WaitForPageLoadAsync()
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task WaitForElementVisibleAsync(string selector)
    {
        await _page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });
    }

    public async Task WaitForElementHiddenAsync(string selector)
    {
        await _page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Hidden
        });
    }

    public async Task WaitForUrlAsync(string urlPart)
    {
        await _page.WaitForURLAsync($"**{urlPart}**");
    }

    public async Task WaitForTimeoutAsync(int milliseconds)
    {
        await _page.WaitForTimeoutAsync(milliseconds);
    }
}