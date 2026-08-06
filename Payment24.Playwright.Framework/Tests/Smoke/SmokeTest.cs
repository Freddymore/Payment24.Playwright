using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Dashboard;

namespace Payment24.Playwright.Framework.Tests.Smoke;

[TestClass]
public class SmokeTest : BaseTest
{
    [TestMethod]
    public async Task Login_To_IMPL_Merchant()
    {
        await StartPortalSessionAsync("IMPL");

        var dashboard = new DashboardPage(Page);

        await dashboard.VerifyDashboardLoadedAsync();

        await dashboard.VerifyDashboardResponseTimeAsync();

        Assert.IsFalse(
            Page.Url.Contains("Login.aspx"),
            "Login failed. User is still on the Login page.");

        Console.WriteLine($"Logged in successfully.");
        Console.WriteLine($"Current Page: {Page.Url}");
    }
}