using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class DriverWalletBalancesPage
    {
        private readonly IPage _page;

        public DriverWalletBalancesPage(IPage page)
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

        private ILocator CustomersBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn2");

        private ILocator CustomerWalletsBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn3");

        private ILocator DriverWalletsBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn4");

        private ILocator StatusDropdown =>
            _page.Locator("#cphBody_ddlStatus");

        private ILocator DriverWalletDropdown =>
            _page.Locator("#cphBody_selectDriverWallet");

        private ILocator SearchField =>
            _page.Locator("input[type='search']:visible").First;

        private ILocator ExportButton =>
            _page.Locator(
                "button.btn.btn-info.dropdown-toggle");

        private ILocator ShowEntriesDropdown =>
            _page.Locator(
                "select[name='cphBody_gridDriverWallet_length']");

        private ILocator DriverWalletTable =>
            _page.Locator("#cphBody_gridDriverWallet");

        private ILocator TableHeaders =>
            DriverWalletTable.Locator("thead tr th");

        private ILocator TableRows =>
            DriverWalletTable.Locator("tbody tr:visible");

        private ILocator TopUpBalanceLink =>
            _page.Locator(
                "a[data-original-title='Top up balance']:visible")
            .First;

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

            // --------------------------------------------------------
            // Customer Management Icon
            // --------------------------------------------------------

            await CustomerManagementIcon.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await CustomerManagementIcon.IsVisibleAsync(),
                "Customer Management icon is not visible.");

            Console.WriteLine(
                "Driver Wallet Customer Management icon verified.");

            // --------------------------------------------------------
            // Heading
            // --------------------------------------------------------

            string heading =
                (await PageHeading.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Driver Wallet Balances",
                heading,
                "Driver Wallet Balances heading is incorrect.");

            Console.WriteLine(
                "Driver Wallet Balances heading verified.");

            // --------------------------------------------------------
            // Breadcrumbs
            // --------------------------------------------------------

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
                "Customers breadcrumb verified.");

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
                "Customer Wallets breadcrumb verified.");

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
                "Driver Wallets breadcrumb verified.");
        }

        // ============================================================
        // STATUS DROPDOWN
        // ============================================================

        public async Task SelectStatusAsync(string status)
        {
            await StatusDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await StatusDropdown.SelectOptionAsync(status);

            Console.WriteLine(
                $"Driver Wallet status selected: {status}");
        }

        // ============================================================
        // VERIFY STATUS OPTIONS
        // ============================================================

        public async Task VerifyStatusOptionsAsync()
        {
            int expectedOptions = 9;

            int actualOptions =
                await StatusDropdown.Locator("option").CountAsync();

            Assert.AreEqual(
                expectedOptions,
                actualOptions,
                $"Expected {expectedOptions} status options but found {actualOptions}.");

            Console.WriteLine(
                $"{actualOptions} Driver Wallet status options verified.");
        }

        // ============================================================
        // DRIVER WALLET DROPDOWN
        // ============================================================

        public async Task SelectDriverWalletAsync(string walletType)
        {
            await DriverWalletDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await DriverWalletDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = walletType
                });

            Console.WriteLine(
                $"Driver Wallet type selected: {walletType}");
        }

        // ============================================================
        // EXPORT BUTTON
        // ============================================================

        public async Task VerifyExportButtonAsync()
        {
            await ExportButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await ExportButton.IsVisibleAsync(),
                "Export button is not displayed.");

            Console.WriteLine(
                "Export button is displayed.");
        }

        // ============================================================
        // SHOW ENTRIES
        // ============================================================

        public async Task VerifyShowEntriesOptionsAsync()
        {
            await ShowEntriesDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            int expectedOptions = 4;

            int actualOptions =
                await ShowEntriesDropdown.Locator("option").CountAsync();

            Assert.AreEqual(
                expectedOptions,
                actualOptions,
                $"Expected {expectedOptions} Show Entries options but found {actualOptions}.");

            Console.WriteLine(
                $"{actualOptions} Show Entries options verified.");
        }

        // ============================================================
        // SEARCH / FILTER TABLE
        // ============================================================

        public async Task SearchAsync(string searchText)
        {
            await SearchField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await SearchField.FillAsync(searchText);

            // Allow DataTables to process the filter.
            await _page.WaitForTimeoutAsync(1000);

            Console.WriteLine(
                $"Driver Wallet table search applied: {searchText}");
        }

        // ============================================================
        // TABLE HEADERS
        // ============================================================

        public async Task VerifyTableHeadersAsync()
        {
            string[] expectedHeaders =
            {
                "Name",
                "Department",
                "Status",
                "Balance",
                "Credit",
                "Debit",
                ""
            };

            await DriverWalletTable.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            int actualHeaderCount =
                await TableHeaders.CountAsync();

            Assert.AreEqual(
                expectedHeaders.Length,
                actualHeaderCount,
                "Driver Wallet table header count is incorrect.");

            for (int i = 0; i < expectedHeaders.Length; i++)
            {
                string actualHeader =
                    (await TableHeaders.Nth(i).InnerTextAsync()).Trim();

                Assert.AreEqual(
                    expectedHeaders[i],
                    actualHeader,
                    $"Driver Wallet table header at position {i + 1} is incorrect.");

                Console.WriteLine(
                    $"Column {i + 1}: '{actualHeader}' verified.");
            }

            Console.WriteLine(
                "Driver Wallet table headers verified.");
        }

        // ============================================================
        // TABLE DATA
        // ============================================================

        public async Task VerifyTableDataAsync()
        {
            var firstRow = TableRows.First;

            await firstRow.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var cells = firstRow.Locator("td");

            string[] columnNames =
            {
                "Name",
                "Department",
                "Status",
                "Balance",
                "Credit",
                "Debit"
            };

            int actualCellCount =
                await cells.CountAsync();

            Assert.IsTrue(
                actualCellCount >= columnNames.Length,
                $"Expected at least {columnNames.Length} data columns but found {actualCellCount}.");

            for (int i = 0; i < columnNames.Length; i++)
            {
                string value =
                    (await cells.Nth(i).InnerTextAsync()).Trim();

                Assert.IsFalse(
                    string.IsNullOrWhiteSpace(value),
                    $"{columnNames[i]} column value is empty.");

                Console.WriteLine(
                    $"{columnNames[i]} column value verified: {value}");
            }

            Console.WriteLine(
                "Driver Wallet table data verified.");
        }

        // ============================================================
        // TOP UP BALANCE LINK
        // ============================================================

        public async Task VerifyTopUpBalanceLinkAsync()
        {
            await TopUpBalanceLink.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await TopUpBalanceLink.IsVisibleAsync(),
                "Top up balance link is not visible.");

            string tagName =
                await TopUpBalanceLink.EvaluateAsync<string>(
                    "element => element.tagName");

            Assert.AreEqual(
                "A",
                tagName,
                "Top up balance element is not a link.");

            string title =
                await TopUpBalanceLink.GetAttributeAsync(
                    "data-original-title") ?? string.Empty;

            Assert.AreEqual(
                "Top up balance",
                title,
                "Top up balance link title is incorrect.");

            Console.WriteLine(
                "Top up balance link verified.");
        }
    }
}