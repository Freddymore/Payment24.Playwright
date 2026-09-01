using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Configuration;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Models;
using Payment24.Playwright.Framework.Pages.Authentication;
using Payment24.Playwright.Framework.Pages.Customers;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class DriverListTests : BaseTest
    {
        [TestMethod]
        public async Task Driver_List_Page_Loads()
        {
            // ============================================================
            // TEST USER
            // ============================================================

            var user = TestUsers.GetUser("BPCRT");

            // ============================================================
            // LOGIN
            // ============================================================

            var loginPage = new LoginPage(Page);

            await loginPage.NavigateToLoginPageAsync(
                $"https://admin-stage.payment24.co/login.aspx?code={user.MerchantCode}");

            await loginPage.VerifyLogoAsync(user.Logo);

            await loginPage.LoginAsync(user);

            Console.WriteLine(
                $"✔ Logged into merchant: {user.MerchantCode}");

            // ============================================================
            // DRIVER LIST PAGE
            // ============================================================

            var driverListPage = new DriverListPage(Page);

            await driverListPage.NavigateToDriverListAsync();

            // ============================================================
            // PAGE RESPONSE TIME
            // ============================================================

            var responseTime = await Page.EvaluateAsync<double>(
                @"() => {
                    if (window.performance &&
                        window.performance.timing &&
                        window.performance.timing.domContentLoadedEventEnd > 0) {
                        return window.performance.timing.domContentLoadedEventEnd -
                               window.performance.timing.navigationStart;
                    }

                    return 0;
                }");

            const int expectedTime = 10000;

            Assert.IsTrue(
                responseTime <= expectedTime,
                $"Page response time ({responseTime} ms) exceeds the threshold of {expectedTime} ms");

            Console.WriteLine(
                $"Driver List page loaded in {responseTime} ms");

            // ============================================================
            // TEST DATA
            // ============================================================

            string fleetName =
                "DO NOT DELETE(BP HO)";

            string status =
                "All";

            string filterValue =
                "27729053339";

            // ============================================================
            // VERIFY PAGE
            // ============================================================

            await driverListPage.VerifyDriverListPageAsync(
                fleetName,
                status,
                filterValue);

            // ============================================================
            // COMPLETED
            // ============================================================

            Console.WriteLine();
            Console.WriteLine(
                "✔ Driver List page smoke test completed successfully.");
        }
    }
}
