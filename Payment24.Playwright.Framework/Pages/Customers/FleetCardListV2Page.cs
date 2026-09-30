
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class FleetCardListV2Page
    {
        private readonly IPage _page;

        public FleetCardListV2Page(IPage page)
        {
            _page = page;
        }

        // ============================================================
        // LOCATORS
        // ============================================================

        private ILocator PageHeading =>
            _page.Locator("h2.heading");

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

        private ILocator SearchButton =>
            _page.Locator("#cphBody_btnSearch");

        private ILocator AddVehicleButton =>
            _page.Locator("#cphBody_btnAdd");

        private ILocator ShowEntriesDropdown =>
            _page.Locator(
                "select[name='cphBody_gridFleetCardList_length']");

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
                "https://admin-stage.payment24.co/FleetCardListV2.aspx",
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 15000
                });

            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            Console.WriteLine(
                "✔ Fleet Card List V2 page navigation completed.");
        }

        // ============================================================
        // PAGE VERIFICATION
        // ============================================================

        public async Task VerifyPageLoadedAsync()
        {
            Assert.IsTrue(
                _page.Url.Contains("FleetCardListV2.aspx"),
                $"Fleet Card List V2 page was not loaded. Current URL: {_page.Url}");

            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 15000
                });

            Console.WriteLine("✔ Fleet Card List V2 page loaded.");
        }

        public async Task VerifyHeaderAsync()
        {
            var heading = (await PageHeading.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Vehicles",
                heading,
                "Fleet Card List V2 heading is incorrect.");

            Console.WriteLine("✔ Vehicles heading verified.");
        }

        // ============================================================
        // FLEET SELECTION
        // ============================================================

        public async Task SelectFleetAsync(string fleetName)
        {
            await FleetDropdown.ClickAsync();

            await FleetSearchField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await FleetSearchField.FillAsync(fleetName);

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
            await StatusDropdown.ClickAsync();

            var options = StatusSelect.Locator("option");
            var optionCount = await options.CountAsync();

            Assert.AreEqual(
                18,
                optionCount,
                $"Expected 18 status options but found {optionCount}.");

            Console.WriteLine(
                $"✔ {optionCount} status options available.");

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

            Console.WriteLine("✔ Fleet Card V2 search executed.");
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
                $"✔ {optionCount} Show Entries options available.");
        }

        // ============================================================
        // FILTER
        // ============================================================

        public async Task FilterByCardNumberAsync(string cardNumber)
        {
            var searchInputs = _page.Locator("input[type='search']");

            var count = await searchInputs.CountAsync();

            Console.WriteLine($"✔ Search input count: {count}");

            for (int i = 0; i < count; i++)
            {
                var input = searchInputs.Nth(i);

                Console.WriteLine(
                    $"Search input {i + 1}: " +
                    $"visible={await input.IsVisibleAsync()}, " +
                    $"value='{await input.InputValueAsync()}'");
            }

            Assert.IsTrue(
                count > 0,
                "No DataTables search input was found.");

            var filterField = searchInputs.First;

            await filterField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await filterField.FillAsync(cardNumber);

            // Allow DataTables to process the filtering.
            await _page.WaitForTimeoutAsync(2000);

            Console.WriteLine(
                $"✔ DataTables filter applied: {cardNumber}");

            // Print the current table rows for debugging.
            var rows = FleetCardTable.Locator("tbody tr");

            var rowCount = await rows.CountAsync();

            Console.WriteLine(
                $"✔ Rows after card-number filter: {rowCount}");

            for (int i = 0; i < rowCount; i++)
            {
                var rowText = (await rows.Nth(i).InnerTextAsync()).Trim();

                Console.WriteLine(
                    $"Row {i + 1}: {rowText}");
            }
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
                $"Expected {expectedHeaders.Length} headers but found {headerCount}.");

            for (int i = 0; i < expectedHeaders.Length; i++)
            {
                var actualHeader =
                    (await TableHeaders.Nth(i).InnerTextAsync()).Trim();

                Assert.AreEqual(
                    expectedHeaders[i].Trim(),
                    actualHeader,
                    $"Incorrect header at position {i + 1}.");
            }

            Console.WriteLine("✔ Fleet Card V2 table headers verified.");
            Console.WriteLine("✔ Column strings count is equal to 9.");
        }

        // ============================================================
        // COLUMN DATA
        // ============================================================

        public async Task VerifyColumnDataAsync()
        {
            await FleetCardTable.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 10000
                });

            var rows = FleetCardTable.Locator("tbody tr");

            var rowCount = await rows.CountAsync();

            Console.WriteLine(
                $"✔ Total table rows detected: {rowCount}");

            for (int i = 0; i < rowCount; i++)
            {
                var cells = rows.Nth(i).Locator("td");

                var cellCount = await cells.CountAsync();

                var rowText = (await rows.Nth(i).InnerTextAsync()).Trim();

                Console.WriteLine(
                    $"Row {i + 1}: {cellCount} cells | {rowText}");
            }

            Assert.IsTrue(
                rowCount > 0,
                "No rows were found in the Fleet Card List table.");

            ILocator? dataRow = null;

            for (int i = 0; i < rowCount; i++)
            {
                var cells = rows.Nth(i).Locator("td");

                var cellCount = await cells.CountAsync();

                if (cellCount >= 8)
                {
                    dataRow = rows.Nth(i);
                    break;
                }
            }

            Assert.IsNotNull(
                dataRow,
                "No vehicle data row containing at least 8 cells was found.");

            var dataCells = dataRow!.Locator("td");

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
                var value = (await dataCells.Nth(i).InnerTextAsync()).Trim();

                Assert.IsFalse(
                    string.IsNullOrWhiteSpace(value),
                    $"{columnNames[i]} column value is empty.");

                Console.WriteLine(
                    $"✔ {columnNames[i]}: {value}");
            }

            Console.WriteLine("✔ Fleet Card V2 column data verified.");
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
                    _page.Locator(
                        "a[data-original-title='Vehicle Details']")
                },
                {
                    "Customer Profile",
                    _page.Locator(
                        "a[data-original-title='Customer Profile']")
                },
                {
                    "Transactions",
                    _page.Locator(
                        "a[data-original-title='Transactions']")
                },
                {
                    "Voucher",
                    _page.Locator(
                        "a[data-original-title='Voucher']")
                },
                {
                    "Link Assets",
                    _page.Locator(
                        "a[data-original-title='Link Assets']")
                },
                {
                    "Transfer Vehicle",
                    _page.Locator(
                        "a[data-original-title='Transfer Vehicle']")
                },

                //This two link might be showing on a different merchant, do not delete this line of code instead comment it out
                /*{
                    "Add New Card",
                    _page.Locator(
                        "a[data-original-title='Add New Card']")
                },
                {
                    "View Card",
                    _page.Locator(
                        "a[data-original-title='View Card']")
                }*/
            };

            foreach (var link in links)
            {
                var count = await link.Value.CountAsync();

                Assert.IsTrue(
                    count > 0,
                    $"{link.Key} link was not found.");

                var tagName =
                    await link.Value.First.EvaluateAsync<string>(
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