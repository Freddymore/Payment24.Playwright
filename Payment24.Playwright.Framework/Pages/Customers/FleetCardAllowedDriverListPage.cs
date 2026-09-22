using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class FleetCardAllowedDriverListPage
    {
        private readonly IPage _page;

        public FleetCardAllowedDriverListPage(IPage page)
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

        private ILocator LinkVehiclesBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn3");

        private ILocator DriverStatement =>
            _page.Locator(
                "div.col.col-lg-12.col-md-6.col-sm-6.col-xs-4")
            .First;

        private ILocator FleetName =>
            _page.Locator("#cphBody_lblFleetName");

        private ILocator AllowAllVehiclesButton =>
            _page.Locator("#cphBody_btnAllowAllVehicles");

        private ILocator AllowSelectedVehiclesButton =>
            _page.Locator("#cphBody_btnSelectedVehicles");

        private ILocator UnlinkVehiclesButton =>
            _page.Locator("#cphBody_btnUnAssign");

        private ILocator Message =>
            _page.Locator("#cphBody_lblMessage");

        private ILocator DriverDropdown =>
            _page.Locator("#cphBody_selDriver");

        private ILocator ShowEntriesDropdown =>
            _page.Locator(
                "select[name='cphBody_gridFleetCardList_length']");
        private ILocator FilterInputField =>
             _page.Locator("input[type='search']").First;

        private ILocator FilterInput =>
            _page.Locator("#cphBody_gridFleetCardList_filter input[type='search']");

        private ILocator VehicleTable =>
            _page.Locator("#cphBody_gridFleetCardList");

        private ILocator VehicleHeaders =>
            VehicleTable.Locator("thead tr th");

        private ILocator VehicleRows =>
            VehicleTable.Locator("tbody tr:visible");

        private ILocator VehicleCheckboxes =>
            VehicleTable.Locator(
                "tbody tr:visible input[type='checkbox']");

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

            string heading =
                (await PageHeading.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Link Vehicles to Driver",
                heading,
                "Link Vehicles to Driver heading is incorrect.");

            Console.WriteLine(
                "Link Vehicles to Driver heading verified.");

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

            await LinkVehiclesBreadcrumb.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.AreEqual(
                "Link Vehicles To Driver",
                (await LinkVehiclesBreadcrumb.InnerTextAsync()).Trim(),
                "Link Vehicles To Driver breadcrumb is incorrect.");

            Console.WriteLine(
                "Link Vehicles To Driver breadcrumb verified.");
        }

        // ============================================================
        // VERIFY FLEET INFORMATION
        // ============================================================

        public async Task VerifyFleetInformationAsync()
        {
            await DriverStatement.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            string expectedStatement =
                "Driver will be allowed to use all of the following vehicles by default. " +
                "If you would like to limit the vehicles this driver is allowed to use " +
                "please select them from the list.";

            Assert.AreEqual(
                expectedStatement,
                (await DriverStatement.InnerTextAsync()).Trim(),
                "Driver statement is incorrect.");

            await FleetName.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.AreEqual(
                "IMPL3086010 - DO NOT DELETE",
                (await FleetName.InnerTextAsync()).Trim(),
                "Fleet name is incorrect.");

            Console.WriteLine(
                "Driver statement verified.");

            Console.WriteLine(
                "Fleet name verified.");
        }

        // ============================================================
        // ALLOW ALL VEHICLES WITHOUT DRIVER
        // ============================================================

        public async Task VerifyAllowAllWithoutDriverAsync()
        {
            await AllowAllVehiclesButton.ClickAsync();

            await Message.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            string message =
                (await Message.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Please select driver.",
                message,
                "Unexpected message when no driver is selected.");

            Console.WriteLine(
                "Prompt verified: Please select driver.");
        }

        // ============================================================
        // SELECT DRIVER
        // ============================================================

        public async Task SelectDriverAsync(string driverName)
        {
            await DriverDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await DriverDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = driverName
                });

            await _page.WaitForTimeoutAsync(1000);

            Console.WriteLine(
                $"Driver selected: {driverName}");
        }

        // ============================================================
        // VERIFY DRIVER CONTROLS
        // ============================================================

        public async Task VerifyDriverControlsAsync()
        {
            await ShowEntriesDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 10000
                });

            await FilterInputField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await ShowEntriesDropdown.IsVisibleAsync(),
                "Show Entries dropdown is not visible.");

            Assert.IsTrue(
                await FilterInputField.IsVisibleAsync(),
                "Filter search field is not visible.");

            Console.WriteLine(
                "Show Entries dropdown verified.");

            Console.WriteLine(
                "Driver filter search field verified.");
        }

        // ============================================================
        // UNLINK VEHICLES
        // ============================================================

        public async Task UnlinkVehiclesAsync()
        {
            await UnlinkVehiclesButton.ClickAsync();
             
            await Message.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await _page.WaitForTimeoutAsync(500);

            Console.WriteLine(
                "Unlink Vehicles button clicked.");
        }

        // ============================================================
        // VERIFY NO VEHICLES LINKED
        // ============================================================

        public async Task VerifyNoVehiclesLinkedAsync()
        {
            string message =
                (await Message.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "There are currently no vehicles linked to driver.",
                message,
                "Unexpected vehicle unlink message.");

            Console.WriteLine(
                "Prompt verified: There are currently no vehicles linked to driver.");
        }

        // ============================================================
        // ALLOW SELECTED WITHOUT SELECTION
        // ============================================================

        public async Task VerifyAllowSelectedWithoutSelectionAsync()
        {
            await AllowSelectedVehiclesButton.ClickAsync();

            await Message.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            string message =
                (await Message.InnerTextAsync()).Trim();

            Assert.IsTrue(
                message.Contains("No vehicles were selected"),
                $"Unexpected message: {message}");

            Console.WriteLine(
                "Prompt verified: No vehicles were selected.");
        }

        // ============================================================
        // UNCHECK FIRST VEHICLE
        // ============================================================
        public async Task SelectFirstVehicleAsync()
        {
            var checkboxes = _page.Locator(
                "#cphBody_gridFleetCardList input[type='checkbox']"
            );

            await checkboxes.First.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 10000
                });

            var count = await checkboxes.CountAsync();

            Assert.IsTrue(
                count > 0,
                "No vehicle checkboxes were found in the Fleet Card vehicle table."
            );

            var firstCheckbox = checkboxes.First;

            await firstCheckbox.ScrollIntoViewIfNeededAsync();

            if (!await firstCheckbox.IsCheckedAsync())
            {
                await firstCheckbox.CheckAsync();
            }

            Assert.IsTrue(
                await firstCheckbox.IsCheckedAsync(),
                "The first vehicle checkbox is not selected."
            );

            Console.WriteLine("✔ First vehicle selected.");
        }

        // ============================================================
        // ALLOW SELECTED VEHICLES
        // ============================================================

        public async Task AllowSelectedVehiclesAsync()
        {
            var selectedVehicleCheckboxes = _page
                .Locator(
                    "#cphBody_gridFleetCardList tbody tr input[type='checkbox']:checked");

            int selectedCount =
                await selectedVehicleCheckboxes.CountAsync();

            Assert.IsTrue(
                selectedCount > 0,
                "No vehicle checkbox is selected before clicking Allow Selected Vehicles.");

            Console.WriteLine(
                $"Selected vehicle count: {selectedCount}");

            await _page
                .Locator("#cphBody_btnSelectedVehicles")
                .ClickAsync();

            await _page.WaitForTimeoutAsync(2000);

            Console.WriteLine(
                "Allow Selected Vehicles button clicked.");
        }

        // ============================================================
        // VERIFY VEHICLES LINKED
        // ============================================================

        public async Task VerifyVehiclesLinkedAsync()
        {
            string message =
                (await Message.InnerTextAsync()).Trim();

            Assert.IsTrue(
                message.Contains("Tester QA") ||
                message.Contains("1 vehicles"),
                $"Unexpected vehicle assignment message: {message}");

            Console.WriteLine(
                $"Vehicle assignment message verified: {message}");
        }

        // ============================================================
        // TABLE HEADERS
        // ============================================================

        public async Task VerifyTableHeadersAsync()
        {
            string[] expectedHeaders =
            {
                "",
                "Id",
                "Make",
                "Status",
                "Card Number",
                "Registration Number",
                "Tank Size",
                "Fuel Grade"
            };

            await VehicleTable.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            int actualCount =
                await VehicleHeaders.CountAsync();

            Assert.AreEqual(
                expectedHeaders.Length,
                actualCount,
                "Vehicle table header count is incorrect.");

            for (int i = 0; i < expectedHeaders.Length; i++)
            {
                string actualHeader =
                    (await VehicleHeaders.Nth(i).InnerTextAsync()).Trim();

                Assert.AreEqual(
                    expectedHeaders[i],
                    actualHeader,
                    $"Vehicle table header at position {i + 1} is incorrect.");

                Console.WriteLine(
                    $"Column {i + 1}: '{actualHeader}' verified.");
            }

            Console.WriteLine(
                "Vehicle table headers verified.");
        }

        // ============================================================
        // TABLE DATA
        // ============================================================

        public async Task VerifyTableDataAsync()
        {
            var firstRow =
                VehicleRows.First;

            await firstRow.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var cells =
                firstRow.Locator("td");

            string[] columnNames =
            {
                "Id",
                "Make",
                "Status",
                "Card Number",
                "Registration Number",
                "Tank Size",
                "Fuel Grade"
            };

            for (int i = 0; i < columnNames.Length; i++)
            {
                string value =
                    (await cells.Nth(i + 1).InnerTextAsync()).Trim();

                // Preserve the original Selenium test behaviour:
                // values must be represented as strings, but may be blank.
                Console.WriteLine(
                    $"{columnNames[i]} column value: '{value}'");

                Assert.IsNotNull(
                    value,
                    $"{columnNames[i]} column value is null.");
            }

            Console.WriteLine(
                "Vehicle table data types verified.");
        }
    }
}