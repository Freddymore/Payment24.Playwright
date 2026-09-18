using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class DriverWalletBalancesTests : BaseTest
    {
        [TestMethod]
        public async Task Driver_Wallet_Balances_Page_Loads()
        {
            // ============================================================
            // LOGIN
            // ============================================================

            await StartPortalSessionAsync("BPCRT");

            // ============================================================
            // CUSTOMER WALLET BALANCES PAGE
            // ============================================================

            var customerWalletBalancesPage =
                new CustomerWalletBalancesPage(Page);

            await customerWalletBalancesPage.NavigateToAsync();

            await customerWalletBalancesPage.VerifyPageAsync();

            // ============================================================
            // SEARCH FOR DRIVER
            // ============================================================

            await customerWalletBalancesPage.SearchDriverAsync(
                "Thobani");

            // ============================================================
            // CLICK DRIVER WALLET
            // ============================================================

            await customerWalletBalancesPage.ClickDriverWalletAsync();

            // ============================================================
            // DRIVER WALLET BALANCES PAGE
            // ============================================================

            var driverWalletBalancesPage =
                new DriverWalletBalancesPage(Page);

            await driverWalletBalancesPage.VerifyPageAsync();

            // ============================================================
            // STATUS
            // ============================================================

            await driverWalletBalancesPage.SelectStatusAsync(
                "ACT");

            await driverWalletBalancesPage.VerifyStatusOptionsAsync();

            // ============================================================
            // DRIVER WALLET
            // ============================================================

            await driverWalletBalancesPage.SelectDriverWalletAsync(
                "Click n Collect");

            // ============================================================
            // EXPORT
            // ============================================================

            await driverWalletBalancesPage.VerifyExportButtonAsync();

            // ============================================================
            // SHOW ENTRIES
            // ============================================================

            await driverWalletBalancesPage.VerifyShowEntriesOptionsAsync();

            // ============================================================
            // TABLE SEARCH
            // ============================================================

            await driverWalletBalancesPage.SearchAsync(
                "Thob");

            // ============================================================
            // TABLE HEADERS
            // ============================================================

            await driverWalletBalancesPage.VerifyTableHeadersAsync();

            // ============================================================
            // TABLE DATA
            // ============================================================

            await driverWalletBalancesPage.VerifyTableDataAsync();

            // ============================================================
            // TOP UP BALANCE
            // ============================================================

            await driverWalletBalancesPage.VerifyTopUpBalanceLinkAsync();

            Console.WriteLine();
            Console.WriteLine(
                "================================================");

            Console.WriteLine(
                "DRIVER WALLET BALANCES TEST PASSED");

            Console.WriteLine(
                "================================================");
        }
    }
}