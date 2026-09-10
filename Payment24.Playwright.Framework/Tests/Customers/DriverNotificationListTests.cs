using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Configuration;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages;
using Payment24.Playwright.Framework.Pages.Authentication;
using Payment24.Playwright.Framework.Pages.Customers;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class DriverNotificationListTests : BaseTest
    {
        [TestMethod]
        public async Task Driver_Notification_List_Page_Loads()
        {
            // ============================================================
            // LOGIN PAGE
            // ============================================================

            var loginPage = new LoginPage(Page);
            var user = TestUsers.GetUser("RUBE");

            await loginPage.NavigateToLoginPageAsync(
                "https://admin-stage.payment24.co/login.aspx?code=RUBE");

            // ============================================================
            // VERIFY RUBE LOGO
            // ============================================================

            var rubeLogo =
                Page.Locator("img[src='images/RUBELogo.png']");

            await rubeLogo.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.IsTrue(
                await rubeLogo.IsVisibleAsync(),
                "RUBE merchant logo is not displayed.");

            string? logoSrc =
                await rubeLogo.GetAttributeAsync("src");

            Assert.IsNotNull(
                logoSrc,
                "RUBE logo src is null.");

            Assert.IsTrue(
                logoSrc.Contains("RUBELogo"),
                $"Unexpected logo source: {logoSrc}");

            Console.WriteLine(
                $"✔ RUBE merchant logo verified. Logo Source: {logoSrc}");

            // ============================================================
            // PERFORM LOGIN
            // ============================================================

            await Page.Locator("#txtUserName")
                .FillAsync(user.Username);

            await Page.Locator("#txtPassword")
                .FillAsync(user.Password);

            await Page.Locator("#BtnLogin")
                .ClickAsync();

            // Login is known to take time.
            // Wait for the login page transition instead of NetworkIdle.
            await Page.Locator("#txtUserName").WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Hidden,
                    Timeout = 60000
                });

            Console.WriteLine(
                "✔ RUBE login completed.");

            // ============================================================
            // ACCEPT COOKIES
            // ============================================================

            var cookieButton =
                Page.Locator(
                    "button.btn.btn-primary.btn-sm.acceptcookies");

            if (await cookieButton.IsVisibleAsync())
            {
                await cookieButton.ClickAsync();

                Console.WriteLine(
                    "✔ Cookies accepted.");
            }
            else
            {
                Console.WriteLine(
                    "✔ Cookie button not present - possibly already accepted.");
            }

            // ============================================================
            // DRIVER NOTIFICATION LIST
            // ============================================================

            var notificationPage =
                new DriverNotificationListPage(Page);

            await notificationPage.NavigateAsync();

            await notificationPage.VerifyPageLoadTimeAsync();

            await notificationPage.VerifyStringsAndLinksAsync();
        }
    }
}