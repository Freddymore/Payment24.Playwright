using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class FleetCardListPage
    {
        private readonly IPage _page;

        public FleetCardListPage(IPage page)
        {
            _page = page;
        }

        // ============================================================
        // LOCATORS
        // ============================================================

        private ILocator PageHeading =>
            _page.Locator("h2.heading");

        private ILocator CustomerGroupDropdown =>
            _page.Locator("#select2-cphBody_selCustomerGroup-container");

        private ILocator FleetDropdown =>
            _page.Locator("#select2-ddlFleet-container");

        private ILocator FleetSearchField =>
            _page.Locator("input.select2-search__field");

        private ILocator FleetResults =>
            _page.Locator("#select2-ddlFleet-results li").First;

        private ILocator StatusDropdown =>
            _page.Locator("#select2-cphBody_ddlStatus-container");

        private ILocator StatusSelect =>
            _page.Locator("#cphBody_ddlStatus");

        private ILocator StatusResults =>
            _page.Locator("#select2-cphBody_ddlStatus-results li").First;

        private ILocator SearchField =>
            _page.Locator("#cphBody_txtSearch");

        private ILocator AddVehicleButton =>
            _page.Locator("#cphBody_btnAdd");

        private ILocator SearchButton =>
            _page.Locator("#cphBody_btnSearch");

        private ILocator ShowEntriesDropdown =>
            _page.Locator("select[name='cphBody_gridFleetCardList_length']");

        private ILocator FilterField =>
            _page.Locator("input[type='search']").First;

        private ILocator FleetCardTable =>
            _page.Locator("#cphBody_gridFleetCardList");

        private ILocator TableHeaders =>
            FleetCardTable.Locator("thead tr th");

        private ILocator FirstDataRow =>
            FleetCardTable.Locator("tbody tr").First;

        // ============================================================
        // NAVIGATION
        // ============================================================

        public async Task NavigateAsync()
        {
            await _page.GotoAsync(
                "https://admin-stage.payment24.co/FleetCardList.aspx",
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 15000
                });

            Console.WriteLine("✔ Fleet Card List page navigation completed.");
        }

        // ============================================================
        // PAGE VERIFICATION
        // ============================================================

        public async Task VerifyPageLoadedAsync()
        {
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            Assert.IsTrue(
                _page.Url.Contains("FleetCardList.aspx"),
                $"Fleet Card List page was not loaded. Current URL: {_page.Url}");

            Console.WriteLine("✔ Fleet Card List page loaded.");
        }

        public async Task VerifyHeaderAsync()
        {
            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 15000
                });

            var heading = (await PageHeading.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Vehicles",
                heading,
                "Fleet Card List page heading is incorrect.");

            Console.WriteLine("✔ Vehicles heading verified.");
        }

        // ============================================================
        // FLEET SELECTION
        // ============================================================

        public async Task SelectFleetAsync(string fleetName)
        {
            await FleetDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await FleetDropdown.ClickAsync();

            await FleetSearchField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await FleetSearchField.FillAsync(fleetName);

            await _page.WaitForTimeoutAsync(1000);

            await FleetResults.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await FleetResults.ClickAsync();

            Console.WriteLine($"✔ Fleet selected: {fleetName}");
        }

        // ============================================================
        // STATUS
        // ============================================================

        public async Task VerifyStatusDropdownAsync()
        {
            await StatusDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await StatusDropdown.ClickAsync();

            var options = StatusSelect.Locator("option");

            var optionCount = await options.CountAsync();

            Assert.AreEqual(
                18,
                optionCount,
                $"Expected 18 status options but found {optionCount}.");

            Console.WriteLine(
                $"✔ {optionCount} Number of statuses available for selection.");

            await StatusResults.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await StatusResults.ClickAsync();

            Console.WriteLine("✔ Status selected.");
        }

        // ============================================================
        // SEARCH
        // ============================================================

        public async Task SearchAsync()
        {
            await SearchField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await SearchButton.ClickAsync();

            await _page.WaitForTimeoutAsync(1500);

            Console.WriteLine("✔ Fleet Card search executed.");
        }

        public async Task FilterByCardNumberAsync(string cardNumber)
        {
            await FilterField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await FilterField.FillAsync(cardNumber);

            await _page.WaitForTimeoutAsync(1500);

            Console.WriteLine($"✔ Fleet Card table filtered by card number: {cardNumber}");
        }

        // ============================================================
        // SHOW ENTRIES
        // ============================================================

        public async Task VerifyShowEntriesAsync()
        {
            await ShowEntriesDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 10000
                });

            var options = ShowEntriesDropdown.Locator("option");

            var optionCount = await options.CountAsync();

            Assert.AreEqual(
                4,
                optionCount,
                $"Expected 4 Show Entries options but found {optionCount}.");

            Console.WriteLine(
                $"✔ {optionCount} Number of Show Entries available for selection.");
        }

        // ============================================================
        // TABLE HEADERS
        // ============================================================

        public async Task VerifyTableHeadersAsync()
        {
            string[] expectedHeaders =
            {
                "Make",
                "Status",
                "Department",
                "Card Number",
                "Registration Number",
                "Reference Number",
                "Tank Size",
                "Fuel Grade",
                " "
            };

            await TableHeaders.First.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 10000
                });

            var headerCount = await TableHeaders.CountAsync();

            Assert.AreEqual(
                expectedHeaders.Length,
                headerCount,
                $"Expected {expectedHeaders.Length} table columns but found {headerCount}.");

            for (int i = 0; i < expectedHeaders.Length; i++)
            {
                var actualHeader = (await TableHeaders.Nth(i).InnerTextAsync()).Trim();

                Assert.AreEqual(
                    expectedHeaders[i].Trim(),
                    actualHeader,
                    $"Incorrect table header at position {i + 1}.");
            }

            Console.WriteLine("✔ Fleet Card List table headers verified.");
            Console.WriteLine("✔ Column strings count is equal to 9.");
        }

        // ============================================================
        // COLUMN DATA
        // ============================================================

        public async Task VerifyColumnDataAsync()
        {
            await FirstDataRow.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var cells = FirstDataRow.Locator("td");

            var cellCount = await cells.CountAsync();

            Assert.IsTrue(
                cellCount >= 8,
                $"Expected at least 8 data columns but found {cellCount}.");

            string[] columnNames =
            {
                "Make",
                "Status",
                "Department",
                "Card Number",
                "Registration Number",
                "Reference Number",
                "Tank Size",
                "Fuel Grade"
            };

            for (int i = 0; i < columnNames.Length; i++)
            {
                var value = (await cells.Nth(i).InnerTextAsync()).Trim();

                Assert.IsFalse(
                    string.IsNullOrWhiteSpace(value),
                    $"{columnNames[i]} column value is empty.");

                Console.WriteLine(
                    $"✔ {columnNames[i]} column value is a string.");
            }
        }

        // ============================================================
        // ACTION LINKS
        // ============================================================

        public async Task VerifyActionLinksAsync()
        {
            var links = new Dictionary<string, ILocator>
            {
                {
                    "Vehicle Details",
                    _page.Locator("a[data-original-title='Vehicle Details']")
                },
                {
                    "Customer Profile",
                    _page.Locator("a[data-original-title='Customer Profile']")
                },
                {
                    "Transactions",
                    _page.Locator("a[data-original-title='Transactions']")
                },
                {
                    "Link Assets",
                    _page.Locator("a[data-original-title='Link Assets']")
                },
                {
                    "Transfer Vehicle",
                    _page.Locator("a[data-original-title='Transfer Vehicle']")
                },
                {
                    "Add New Card",
                    _page.Locator("a[data-original-title='Add New Card']")
                },
                {
                    "View Card",
                    _page.Locator("a[data-original-title='View Card']")
                }
            };

            foreach (var link in links)
            {
                var count = await link.Value.CountAsync();

                Assert.IsTrue(
                    count > 0,
                    $"{link.Key} link was not found.");

                var tagName = await link.Value.First.EvaluateAsync<string>(
                    "element => element.tagName.toLowerCase()");

                Assert.AreEqual(
                    "a",
                    tagName,
                    $"{link.Key} is not an anchor link.");

                Console.WriteLine(
                    $"✔ {link.Key} button is a link.");
            }
        }
    }
}