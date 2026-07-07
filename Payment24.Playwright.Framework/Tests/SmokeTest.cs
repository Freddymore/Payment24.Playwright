using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;

namespace Payment24.Playwright.Framework.Tests;

[TestClass]
public class SmokeTest : BaseTest
{
    [TestMethod]
    public async Task BrowserShouldOpen()
    {
        await Page.GotoAsync("https://www.google.com");

        Assert.AreEqual(
            "Google",
            await Page.TitleAsync());
    }
}