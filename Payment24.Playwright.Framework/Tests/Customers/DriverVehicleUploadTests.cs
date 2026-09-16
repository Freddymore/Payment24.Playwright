using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Configuration;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Authentication;
using Payment24.Playwright.Framework.Pages.Customers;
using System;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class DriverVehicleUploadTests : BaseTest
    {
        [TestMethod]
        public async Task Driver_Vehicle_Upload()
        {
            // ========================================================
            // LOGIN TO BPCRT
            // ========================================================

            var loginPage = new LoginPage(Page);
            var user = TestUsers.GetUser("BPCRT");

            await loginPage.NavigateToLoginPageAsync(
                "https://admin-stage.payment24.co/Login.aspx?code=BPCRT");

            // Verify merchant logo
            var logo =
                Page.Locator("img[src='images/Payment24-Logo.png']");

            await logo.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.IsTrue(
                await logo.IsVisibleAsync(),
                "Payment24 merchant logo is not displayed.");

            Console.WriteLine(
                "✔ Payment24 merchant logo verified.");

            // Login
            await Page.Locator("#txtUserName")
                .FillAsync(user.Username);

            await Page.Locator("#txtPassword")
                .FillAsync(user.Password);

            // Display password
            await Page.Locator("#test").ClickAsync();

            // Login
            await Page.Locator("#BtnLogin").ClickAsync();

            // Wait until login page is gone
            await Page.Locator("#txtUserName").WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Hidden,
                    Timeout = 60000
                });

            Console.WriteLine(
                "✔ BPCRT login successful.");


            // ========================================================
            // ACCEPT COOKIES
            // ========================================================

            var cookieButton =
                Page.Locator(
                    "button.btn.btn-primary.btn-sm.acceptcookies");

            try
            {
                await cookieButton.WaitForAsync(
                    new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 5000
                    });

                await cookieButton.ClickAsync();

                Console.WriteLine(
                    "✔ Cookies accepted.");
            }
            catch (TimeoutException)
            {
                Console.WriteLine(
                    "✔ Cookie popup not displayed.");
            }


            // ========================================================
            // DRIVER VEHICLE UPLOAD
            // ========================================================

            var driverVehicleUploadPage =
                new DriverVehicleUploadPage(Page);

            await driverVehicleUploadPage.NavigateAsync();

            // Verify page response time
            await driverVehicleUploadPage.VerifyPageLoadTimeAsync();

            // Verify page heading and fields
            await driverVehicleUploadPage.VerifyPageDetailsAsync();


            // ========================================================
            // SELECT CUSTOMER
            // ========================================================

            await driverVehicleUploadPage.SelectCustomerAsync();


            // ========================================================
            // UPLOAD EXCEL TEMPLATE
            // ========================================================

            string filePath =
                @"C:\Users\FreddyMore\Testing Evidence\Payment24_DriverVehicleUpload_Template.xlsx";

            await driverVehicleUploadPage.UploadDriverVehicleFileAsync(
                filePath);


            // ========================================================
            // VIEW UPLOADED DATA
            // ========================================================

            await driverVehicleUploadPage.ClickViewAsync();


            // ========================================================
            // VERIFY TABLE HEADERS
            // ========================================================

            await driverVehicleUploadPage
                .VerifyUploadTableHeadersAsync();


            // ========================================================
            // SEND DRIVER PIN
            // ========================================================

            await driverVehicleUploadPage.SendDriverPinAsync();


            // ========================================================
            // SUBMIT
            // ========================================================

            await driverVehicleUploadPage.SubmitUploadAsync();


            // ========================================================
            // VERIFY SUCCESS
            // ========================================================

            await driverVehicleUploadPage.VerifySuccessMessageAsync();
        }
    }
}