using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class CustomerWalletBalancesPage
    {
        private readonly IPage _page;

        public CustomerWalletBalancesPage(IPage page)
        {
            _page = page;
        }

        // ============================================================
        // LOCATORS
        // ============================================================

        private ILocator PageHeading =>
            _page.Locator("h2.heading");

        private ILocator CustomerManagementIcon =>
            _page.Locator(
                "img[src='images/icons/customer_management_icon@2x.png']");

        private ILocator HouseIcon   =>
            _page.Locator("a.breadcontainer.breadbtn1");    

        private ILocator CustomersBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn2");

        private ILocator CustomerWalletsBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn3");

       /* private ILocator DriverWalletsBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn4");*/

        private ILocator DriverWalletLink =>
            _page.Locator(
                "a[data-original-title='Driver Wallet']");

       /* private ILocator CustomerWalletLink =>
            _page.Locator(
                "a[data-original-title='Customer Wallet']");*/

        private ILocator DataTableSearch =>
            _page.Locator(
                "input[type='search']")
            .First;

        // ============================================================
        // NAVIGATION
        // ============================================================

        public async Task NavigateToAsync()
        {
            await _page.GotoAsync(
                "https://admin-stage.payment24.co/FleetWalletBalances.aspx",
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 30000
                });

            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 15000
                });

            Console.WriteLine(
                "✔ Customer Wallet Balances page loaded.");
        }

        // ============================================================
        // PAGE VERIFICATION
        // ============================================================

        public async Task VerifyPageAsync()
        {
            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 15000
                });

            await CustomerManagementIcon.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await CustomerManagementIcon.IsVisibleAsync(),
                "Customer Management icon is not visible.");

            // Page heading
            string heading =
                (await PageHeading.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Customer Wallet Balances",
                heading,
                "Customer Wallet Balances heading is incorrect.");

            Console.WriteLine(
                "✔ Customer Wallet Balances heading verified.");

            // Verify house icon
            await HouseIcon.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                }); 
            Assert.AreEqual(
                1,
                await HouseIcon.CountAsync(),
                "House icon is not visible.");

            // Customers breadcrumb
            await CustomersBreadcrumb.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.AreEqual(
                "Customers",
                (await CustomersBreadcrumb.InnerTextAsync()).Trim(),
                "Customers breadcrumb is incorrect.");

            Console.WriteLine(
                "✔ Customers breadcrumb verified.");

            // Customer Wallets breadcrumb
            await CustomerWalletsBreadcrumb.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.AreEqual(
                "Customer Wallets",
                (await CustomerWalletsBreadcrumb.InnerTextAsync()).Trim(),
                "Customer Wallets breadcrumb is incorrect.");

            Console.WriteLine(
                "✔ Customer Wallets breadcrumb verified.");

           /* // Driver Wallets breadcrumb
            await DriverWalletsBreadcrumb.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.AreEqual(
                "Driver Wallets",
                (await DriverWalletsBreadcrumb.InnerTextAsync()).Trim(),
                "Driver Wallets breadcrumb is incorrect.");

            Console.WriteLine(
                "✔ Driver Wallets breadcrumb verified.");*/
        }
        
        // ============================================================
        // SEARCH
        // ============================================================

        public async Task SearchDriverAsync(string driverName)
        {
            await DataTableSearch.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await DataTableSearch.FillAsync(driverName);

            await _page.WaitForTimeoutAsync(1000);

            Console.WriteLine(
                $"✔ Customer Wallet search applied: {driverName}");
        }

        // ============================================================
        // DRIVER WALLET LINK
        // ============================================================

        public async Task ClickDriverWalletAsync()
        {
            var driverWalletLink =
                DriverWalletLink.First;

            await driverWalletLink.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await driverWalletLink.IsVisibleAsync(),
                "Driver Wallet link is not visible.");

            await driverWalletLink.ClickAsync();

            Console.WriteLine(
                "✔ Driver Wallet link clicked.");
        }

        // ============================================================
        // CUSTOMER WALLET LINK
        // ============================================================

       /*    public async Task VerifyCustomerWalletLinkAsync()
        {
            var customerWalletLink =
                CustomerWalletLink.First;

            await customerWalletLink.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await customerWalletLink.IsVisibleAsync(),
                "Customer Wallet link is not visible.");

            Console.WriteLine(
                "✔ Customer Wallet link is displayed.");
        }*/
    }
}