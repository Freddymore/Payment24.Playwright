using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class DriverListPage : BasePage
    {
        // ============================================================
        // PAGE LOCATORS
        // ============================================================

        private const string CustomerManagementIcon =
            "img[src*='customer_management_icon']";

        private const string PageHeading =
            "h2.heading";

        private const string CustomersBreadcrumb =
            "a.breadcrumb.breadbtn2";

        private const string DriversBreadcrumb =
            "a.breadcontainer.breadbtn3";

        private const string FleetDropdown =
            "#select2-ddlFleet-container";

        private const string StatusDropdown =
            "#select2-cphBody_ddlStatus-container";

        private const string Select2Search =
            "input.select2-search__field";

        private const string Select2Results =
            "ul.select2-results__options li";

        private const string SearchField =
            "#cphBody_txtSearch";

        private const string SearchButton =
            "#cphBody_btnSearch";

        private const string ExportButton =
            "#cphBody_btnExport";

        private const string ResetButton =
            "#cphBody_btnResetSearch";

        private const string AddDriverButton =
            "#cphBody_btnAddDriver";

        private const string StartDateField =
            "#cphBody_txtFilterStartDate";

        private const string EndDateField =
            "#cphBody_txtFilterEndDate";

        private const string DriverGrid =
            "#cphBody_grvDriverList";

        private const string GridRows =
            "#cphBody_grvDriverList tbody tr";

        private const string GridHeaders =
            "#cphBody_grvDriverList thead tr th";

        private const string GridFilter =
            "input[type='search']";

        private const string ShowEntries =
            "#cphBody_grvDriverList_length select";

        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public DriverListPage(IPage page)
            : base(page)
        {
        }

        // ============================================================
        // PAGE NAVIGATION
        // ============================================================

        public async Task NavigateToDriverListAsync()
        {
            await Page.GotoAsync(
                "https://admin-stage.payment24.co/DriverList.aspx",
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded
                });

            await WaitForPageLoadAsync();

            Console.WriteLine("✔ Driver Invite page loaded.");
        }

        // ============================================================
        // PAGE DETAILS
        // ============================================================

        public async Task VerifyMainPageDetailsAsync()
        {
            Console.WriteLine();
            Console.WriteLine("========== MAIN PAGE DETAILS ==========");

            Assert.IsTrue(
                await Page.Locator(CustomerManagementIcon).IsVisibleAsync(),
                "Customer Management icon is not visible.");

            Console.WriteLine("✔ Customer Management icon verified.");

            var heading = Page.Locator(PageHeading);

            Assert.IsTrue(
                await heading.IsVisibleAsync(),
                "Drivers heading is not visible.");

            Assert.AreEqual(
                "Drivers",
                (await heading.InnerTextAsync()).Trim(),
                "Incorrect page heading.");

            Console.WriteLine("✔ Drivers heading verified.");

            var customersBreadcrumb = Page.Locator(CustomersBreadcrumb);

            Assert.AreEqual(
                "Customers",
                (await customersBreadcrumb.InnerTextAsync()).Trim(),
                "Incorrect Customers breadcrumb.");

            Console.WriteLine("✔ Customers breadcrumb verified.");

            var driversBreadcrumb = Page.Locator(DriversBreadcrumb);

            Assert.AreEqual(
                "Drivers",
                (await driversBreadcrumb.InnerTextAsync()).Trim(),
                "Incorrect Drivers breadcrumb.");

            Console.WriteLine("✔ Drivers breadcrumb verified.");
        }

        // ============================================================
        // SEARCH CONTROLS
        // ============================================================

        public async Task SelectFleetAsync(string fleetName)
        {
            await Page.Locator(FleetDropdown).ClickAsync();

            var search = Page.Locator(Select2Search).Last;

            await search.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

            await search.FillAsync(fleetName);

            var option = Page.Locator(Select2Results)
                .Filter(new LocatorFilterOptions
                {
                    HasTextString = fleetName
                })
                .First;

            await option.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

            await option.ClickAsync();

            Console.WriteLine($"✔ Fleet selected: {fleetName}");
        }

        public async Task SelectStatusAsync(string status)
        {
            await Page.Locator(StatusDropdown).ClickAsync();

            var search = Page.Locator(Select2Search).Last;

            await search.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

            await search.FillAsync(status);

            var option = Page.Locator(Select2Results)
                .Filter(new LocatorFilterOptions
                {
                    HasTextString = status
                })
                .First;

            await option.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

            await option.ClickAsync();

            Console.WriteLine($"✔ Status selected: {status}");
        }

        public async Task VerifySearchControlsAsync(
            string fleetName,
            string status)
        {
            Console.WriteLine();
            Console.WriteLine("========== SEARCH CONTROLS ==========");

            await SelectFleetAsync(fleetName);

            Assert.IsTrue(
                await Page.Locator(SearchField).IsVisibleAsync(),
                "Search field is not visible.");

            Console.WriteLine("✔ Search field verified.");

            await SelectStatusAsync(status);

            Assert.IsTrue(
                await Page.Locator(SearchButton).IsVisibleAsync(),
                "Search button is not visible.");

            Console.WriteLine("✔ Search button verified.");

            Assert.IsTrue(
                await Page.Locator(ExportButton).IsVisibleAsync(),
                "Export button is not visible.");

            Console.WriteLine("✔ Export button verified.");

            Assert.IsTrue(
                await Page.Locator(ResetButton).IsVisibleAsync(),
                "Reset button is not visible.");

            Console.WriteLine("✔ Reset button verified.");

            Assert.IsTrue(
                await Page.Locator(AddDriverButton).IsVisibleAsync(),
                "Add Driver button is not visible.");

            Console.WriteLine("✔ Add Driver button verified.");

            Assert.IsTrue(
                await Page.Locator(StartDateField).IsVisibleAsync(),
                "Start Date field is not visible.");

            Console.WriteLine("✔ Start Date field verified.");

            Assert.IsTrue(
                await Page.Locator(EndDateField).IsVisibleAsync(),
                "End Date field is not visible.");

            Console.WriteLine("✔ End Date field verified.");
        }

        // ============================================================
        // SEARCH
        // ============================================================

        public async Task SearchAsync()
        {
            await Page.Locator(SearchButton).ClickAsync();

            await Page.Locator(DriverGrid).WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 15000
                });

            await Page.WaitForTimeoutAsync(1000);

            Console.WriteLine("✔ Driver search completed.");
        }

        // ============================================================
        // GRID
        // ============================================================

        public async Task VerifyGridAsync()
        {
            Console.WriteLine();
            Console.WriteLine("========== GRID ==========");

            Assert.IsTrue(
                await Page.Locator(DriverGrid).IsVisibleAsync(),
                "Driver grid is not visible.");

            Console.WriteLine("✔ Driver grid verified.");

            int rowCount = await Page.Locator(GridRows).CountAsync();

            Assert.IsTrue(
                rowCount > 0,
                "Driver grid contains no rows.");

            Console.WriteLine(
                $"✔ Driver grid contains {rowCount} row(s).");
        }

        // ============================================================
        // FILTER
        // ============================================================

      
    public async Task ApplyGridFilterAsync(string filterValue)
        {
            Console.WriteLine();
            Console.WriteLine("========== GRID FILTER ==========");

            // Target ONLY the Driver List DataTable filter.
            // This avoids the second input[type='search'] belonging to fleetCardRule.
            var filter = Page.Locator(
                "#cphBody_grvDriverList_filter input[type='search']");

            await filter.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

            Assert.IsTrue(
                await filter.IsVisibleAsync(),
                "Driver List grid filter is not visible.");

            Assert.IsTrue(
                await filter.IsEnabledAsync(),
                "Driver List grid filter is not enabled.");

            await filter.FillAsync(filterValue);

            // Allow DataTables to apply the filter
            await Page.WaitForTimeoutAsync(1000);

            Console.WriteLine(
                $"✔ Grid filter value entered: {filterValue}");
        }



        // ============================================================
        // SHOW ENTRIES
        // ============================================================

        public async Task VerifyShowEntriesAsync()
        {
            var showEntries = Page.Locator(ShowEntries);

            Assert.IsTrue(
                await showEntries.IsVisibleAsync(),
                "Show Entries dropdown is not visible.");

            Assert.IsTrue(
                await showEntries.IsEnabledAsync(),
                "Show Entries dropdown is not enabled.");

            Console.WriteLine(
                "✔ Show Entries dropdown is displayed and enabled.");
        }

        // ============================================================
        // GRID HEADERS
        // ============================================================

        public async Task VerifyGridHeadersAsync()
        {
            Console.WriteLine();
            Console.WriteLine("========== GRID HEADERS ==========");

            string[] expectedHeaders =
            {
                "Department",
                "Name",
                "Driver Number",
                "Status",
                "Mobile Number",
                "Email",
                "Card Number",
                "Type",
                "Manager",
                "License Expiry",
                " "
            };

            var headers = Page.Locator(GridHeaders);

            int headerCount = await headers.CountAsync();

            Assert.AreEqual(
                expectedHeaders.Length,
                headerCount,
                "Unexpected number of grid headers.");

            for (int i = 0; i < expectedHeaders.Length; i++)
            {
                string actualHeader =
                    (await headers.Nth(i).InnerTextAsync()).Trim();

                Assert.AreEqual(
                    expectedHeaders[i].Trim(),
                    actualHeader,
                    $"Header {i + 1} is incorrect.");

                Console.WriteLine(
                    $"✔ Header {i + 1}: {actualHeader}");
            }
        }

        // ============================================================
        // DATA TYPE HELPERS
        // ============================================================

        private async Task VerifyColumnStringAsync(
            int columnNumber,
            string columnName,
            bool valueRequired = true)
        {
            var cell = Page.Locator(
                $"{GridRows}:first-child td:nth-child({columnNumber})");

            Assert.IsTrue(
                await cell.CountAsync() > 0,
                $"{columnName} column could not be found.");

            string value = (await cell.InnerTextAsync()).Trim();

            if (string.IsNullOrWhiteSpace(value))
            {
                if (valueRequired)
                {
                    Assert.Fail(
                        $"{columnName} column contains no value.");
                }

                Console.WriteLine(
                    $"✔ {columnName} is a string field. Value: [blank - allowed]");
                return;
            }

            Console.WriteLine(
                $"✔ {columnName} data type is a string. Value: {value}");
        }

        // ============================================================
        // GRID DATA TYPES
        // ============================================================

        public async Task VerifyGridDataTypesAsync()
        {
            Console.WriteLine();
            Console.WriteLine("========== GRID DATA TYPES ==========");

            // Required fields
            await VerifyColumnStringAsync(
                1,
                "Department");

            await VerifyColumnStringAsync(
                2,
                "Name");

            await VerifyColumnStringAsync(
                4,
                "Status");

            await VerifyColumnStringAsync(
                5,
                "Mobile Number");

            await VerifyColumnStringAsync(
                6,
                "Email");

            await VerifyColumnStringAsync(
                8,
                "Type");

            // Optional fields
            // These may legitimately be blank depending on the merchant/driver.
            await VerifyColumnStringAsync(
                3,
                "Driver Number",
                false);

            await VerifyColumnStringAsync(
                7,
                "Card Number",
                false);

            await VerifyColumnStringAsync(
                10,
                "License Expiry",
                false);
        }

        // ============================================================
        // MANAGER CHECKBOX
        // ============================================================

        public async Task VerifyManagerCheckboxAsync()
        {
            Console.WriteLine();
            Console.WriteLine("========== MANAGER COLUMN ==========");

            var managerCheckbox = Page.Locator(
                $"{GridRows}:first-child td:nth-child(9) input[name='ctl00$cphBody$grvDriverList$ctl02$chkFleetManager']");

            Assert.IsTrue(
                await managerCheckbox.CountAsync() > 0,
                "Manager checkbox was not found.");

            Assert.IsTrue(
                await managerCheckbox.IsVisibleAsync(),
                "Manager checkbox is not visible.");

            bool enabled = await managerCheckbox.IsEnabledAsync();

            Assert.IsFalse(
                enabled,
                "Manager checkbox is enabled.");

            Console.WriteLine(
                "✔ Manager checkbox is displayed and not enabled.");
        }

        // ============================================================
        // ACTION LINKS
        // ============================================================

        public async Task VerifyActionLinksAsync()
        {
            Console.WriteLine();
            Console.WriteLine("========== CUSTOMER ACTION LINKS ==========");

            string[] actionTitles =
            {
                "Driver Details",
                "Customer Profile",
                "Link Vehicles to Driver",
                "Transactions",
                "Reset PIN",
                "Voucher",
                "Activate driver for USSD voucher"
            };

            foreach (string title in actionTitles)
            {
                var link = Page.Locator(
                    $"a[data-original-title='{title}']")
                    .First;

                Assert.IsTrue(
                    await link.CountAsync() > 0,
                    $"{title} action link was not found.");

                string tagName =
                    await link.EvaluateAsync<string>(
                        "element => element.tagName.toLowerCase()");

                Assert.AreEqual(
                    "a",
                    tagName,
                    $"{title} is not a link.");

                Console.WriteLine(
                    $"✔ {title} button icon is a link.");
            }
        }

        // ============================================================
        // COMPLETE PAGE VERIFICATION
        // ============================================================

        public async Task VerifyDriverListPageAsync(
            string fleetName,
            string status,
            string filterValue)
        {
            await VerifyMainPageDetailsAsync();

            await VerifySearchControlsAsync(
                fleetName,
                status);

            await SearchAsync();

            await ApplyGridFilterAsync(filterValue);

            await VerifyGridAsync();

            await VerifyShowEntriesAsync();

            await VerifyGridHeadersAsync();

            await VerifyGridDataTypesAsync();

            await VerifyManagerCheckboxAsync();

            await VerifyActionLinksAsync();

            Console.WriteLine();
            Console.WriteLine(
                "✔ Driver List page verified successfully.");
        }
    }
}
