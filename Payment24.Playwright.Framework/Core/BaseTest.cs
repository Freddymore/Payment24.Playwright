using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Configuration;
using Payment24.Playwright.Framework.Pages;
using Payment24.Playwright.Framework.Pages.Authentication;

namespace Payment24.Playwright.Framework.Core;

[TestClass]
public abstract class BaseTest
{
    protected BrowserManager BrowserManager = null!;

    protected IPage Page => BrowserManager.Page;

    [TestInitialize]
    public async Task BaseInitialize()
    {
        BrowserManager = new BrowserManager();

        await BrowserManager.StartBrowserAsync();
    }

    [TestCleanup]
    public async Task BaseCleanup()
    {
        await BrowserManager.CloseBrowserAsync();
    }

    /// <summary>
    /// Starts a logged-in Payment24 portal session.
    /// </summary>
    protected async Task StartPortalSessionAsync(string merchant)
    {
        // Create Login Page
        var loginPage = new LoginPage(Page);

        // Perform the complete login workflow
        await loginPage.LoginToMerchantAsync(merchant);

        // Wait until the portal has finished loading
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // TODO:
        // Accept cookies once CookieBanner component has been created
    }
}