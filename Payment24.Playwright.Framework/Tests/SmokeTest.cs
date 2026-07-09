using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Configuration;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages;

namespace Payment24.Playwright.Framework.Tests;

[TestClass]
public class SmokeTest : BaseTest
{
    [TestMethod]
    public async Task Login_To_IMPL_Merchant()
    {
        // Arrange
        var loginPage = new LoginPage(Page);

        // Get merchant credentials from configuration
        var user = TestUsers.GetUser("IMPL");

        // Navigate to the merchant login page
        await loginPage.NavigateToLoginPageAsync(
            $"https://admin-stage.payment24.co/login.aspx?code={user.MerchantCode}");

        // Verify login page
        Assert.IsTrue(await loginPage.IsLoginPageDisplayedAsync());

        // Verify merchant logo
        await loginPage.VerifyLogoAsync(user.Logo);

        // Login
        await loginPage.LoginAsync(user);

        // TODO:
        // Verify Dashboard
    }
}