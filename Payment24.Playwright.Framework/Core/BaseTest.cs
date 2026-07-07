using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Playwright;

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
}