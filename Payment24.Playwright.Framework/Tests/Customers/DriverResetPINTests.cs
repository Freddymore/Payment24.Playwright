using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Configuration;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Authentication;
using Payment24.Playwright.Framework.Pages.Customers;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class DriverResetPINTests : BaseTest
    {
        [TestMethod]
        public async Task Driver_Reset_PIN()
        {
            // Login to BPCRT
            var loginPage = new LoginPage(Page);
            var user = TestUsers.GetUser("BPCRT");

            await loginPage.NavigateToLoginPageAsync(
                "https://admin-stage.payment24.co/login.aspx?code=BPCRT");

            // Verify Payment24 logo
            var logo = Page.Locator("img[src='images/Payment24-Logo.png']");

            await logo.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 60000
            });

            Assert.IsTrue(
                await logo.IsVisibleAsync(),
                "Payment24 logo is not displayed.");

            Console.WriteLine("✔ Payment24 logo verified.");

            // Perform login directly
            await Page.Locator("#txtUserName").FillAsync(user.Username);
            await Page.Locator("#txtPassword").FillAsync(user.Password);

            // Show password
            await Page.Locator("#test").ClickAsync();

            await Page.Locator("#BtnLogin").ClickAsync();

            // Wait for login page to disappear
            await Page.Locator("#txtUserName").WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Hidden,
                    Timeout = 60000
                });

            Console.WriteLine("✔ Login successful.");

            // Accept cookies if displayed
            var cookieButton =
                Page.Locator("button.btn.btn-primary.btn-sm.acceptcookies");

            try
            {
                await cookieButton.WaitForAsync(
                    new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 5000
                    });

                await cookieButton.ClickAsync();

                Console.WriteLine("✔ Cookies accepted.");
            }
            catch (TimeoutException)
            {
                Console.WriteLine(
                    "✔ Cookie button not displayed - possibly already accepted.");
            }

            // Driver Reset PIN page
            var driverResetPINPage = new DriverResetPINPage(Page);

            await driverResetPINPage.NavigateAsync();

            // Verify Drivers List page load time
            await driverResetPINPage.VerifyPageLoadTimeAsync();

            // Verify page heading, breadcrumbs and icon
            await driverResetPINPage.VerifyPageFieldsAsync();

            // Select customer
            await driverResetPINPage.SelectCustomerAsync();

            // Select status
            await driverResetPINPage.SelectStatusAsync();

            // Verify Driver List form fields
            await driverResetPINPage.VerifyFormFieldsAsync();

            // Search drivers
            await driverResetPINPage.SearchAsync();

            // Verify Show Entries dropdown
            await driverResetPINPage.VerifyShowEntriesAsync();

            // Filter by mobile number
            await driverResetPINPage.FilterByMobileNumberAsync();

            // Click Reset PIN
            await driverResetPINPage.ClickResetPINAsync();

            // Verify Reset PIN modal
            await driverResetPINPage.VerifyResetPINModalAsync();

            // Verify modal labels
            await driverResetPINPage.VerifyResetPINLabelsAsync();

            // Verify driver details
            await driverResetPINPage.VerifyDriverDetailsAsync();

            // Verify No and Yes buttons
            await driverResetPINPage.VerifyResetPINButtonsAsync();

            // Confirm Reset PIN
            await driverResetPINPage.ConfirmResetPINAsync();

            // Verify successful PIN reset
            await driverResetPINPage.VerifySuccessMessageAsync();
        }
    }
}