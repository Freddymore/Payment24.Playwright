using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class CustomerWalletBalancesTests : BaseTest
    {
        [TestMethod]
        public async Task Customer_Wallet_Balances_Page_Loads()
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

            // ============================================================
            // VERIFY PAGE
            // ============================================================

            await customerWalletBalancesPage.VerifyPageAsync();

            // ============================================================
            // SEARCH
            // ============================================================

            await customerWalletBalancesPage.SearchDriverAsync(
                "Thobani");

            // ============================================================
            // VERIFY CUSTOMER WALLET LINK
            // ============================================================

            //await customerWalletBalancesPage.VerifyCustomerWalletLinkAsync();

            // ============================================================
            // OPEN DRIVER WALLET
            // ============================================================

            await customerWalletBalancesPage.ClickDriverWalletAsync();

            Console.WriteLine(
                "✔ Customer Wallet Balances test completed.");
        }
    }
}