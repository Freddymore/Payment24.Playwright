using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class DriverResetPINPage
    {
        private readonly IPage _page;

        public DriverResetPINPage(IPage page)
        {
            _page = page;
        }

        // ============================================================
        // PAGE LOCATORS
        // ============================================================

        private ILocator CustomerManagementIcon =>
            _page.Locator(
                "img[src='images/icons/customer_management_icon@2x.png']");

        private ILocator PageHeading =>
            _page.Locator("h2.heading");

        private ILocator CustomersBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn2");

        private ILocator DriversBreadcrumb =>
            _page.Locator("a.breadcontainer.breadbtn3");

        // ============================================================
        // SEARCH / FILTER LOCATORS
        // ============================================================

        private ILocator FleetDropdown =>
            _page.Locator("#select2-ddlFleet-container");

        private ILocator StatusDropdown =>
            _page.Locator("#select2-cphBody_ddlStatus-container");

        private ILocator SearchButton =>
            _page.Locator("#cphBody_btnSearch");

        private ILocator ShowEntriesDropdown =>
            _page.Locator("#cphBody_grvDriverList_length");

        private ILocator FilterTextField =>
            _page.Locator("input[type='search']").First;

        private ILocator ResetButton =>
            _page.Locator("#cphBody_btnResetSearch");

        private ILocator AddDriverButton =>
            _page.Locator("#cphBody_btnAddDriver");

        private ILocator StartDateField =>
            _page.Locator("#cphBody_txtFilterStartDate");

        private ILocator EndDateField =>
            _page.Locator("#cphBody_txtFilterEndDate");

        // ============================================================
        // RESET PIN MODAL LOCATORS
        // ============================================================

        private ILocator ResetPINButton =>
            _page.Locator(
                "a[data-original-title='Reset PIN']").First;

        // IMPORTANT:
        // The Drivers page contains many modal-content elements.
        // Identify the Reset PIN modal by its unique text.
        private ILocator ResetPINModal =>
            _page.Locator("div.modal-content").Filter(
                new LocatorFilterOptions
                {
                    HasText =
                        "Are you sure you want to reset PIN for this Driver?"
                }).First;

        // Scoped to the Reset PIN modal to avoid strict-mode violations.
        private ILocator ResetPINModalTitle =>
            ResetPINModal.Locator("h5.modal-title");

        private ILocator DriverNameLabel =>
            _page.Locator("#cphBody_LblResetPinDriver");

        private ILocator CellPhoneLabel =>
            _page.Locator("#cphBody_LblResetPinNumber");

        private ILocator CardNumberLabel =>
            _page.Locator("#cphBody_LblResetPinPrCardNumber");

        private ILocator NoButton =>
            _page.Locator("#cphBody_modalClose");

        private ILocator YesButton =>
            _page.Locator("#cphBody_btnResetOk");

        private ILocator SuccessMessage =>
            _page.Locator("p.alert-message");


        // ============================================================
        // NAVIGATE TO DRIVER LIST
        // ============================================================

        public async Task NavigateAsync()
        {
            await _page.GotoAsync(
                "https://admin-stage.payment24.co/DriverList.aspx",
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 60000
                });

            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Console.WriteLine("✔ Drivers List page loaded.");
        }


        // ============================================================
        // VERIFY PAGE LOAD TIME
        // ============================================================

        public async Task VerifyPageLoadTimeAsync()
        {
            long responseTime = await _page.EvaluateAsync<long>(
                @"() =>
                {
                    if (performance.timing)
                    {
                        return Math.round(
                            performance.timing.domContentLoadedEventEnd -
                            performance.timing.navigationStart
                        );
                    }

                    return 0;
                }");

            Console.WriteLine(
                $"{responseTime} ms - Time taken to load the Drivers List page.");

            Assert.IsTrue(
                responseTime <= 10000,
                $"Page response time ({responseTime} ms) exceeds the threshold of 10000 ms.");
        }


        // ============================================================
        // VERIFY PAGE FIELDS
        // ============================================================

        public async Task VerifyPageFieldsAsync()
        {
            await CustomerManagementIcon.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.IsTrue(
                await CustomerManagementIcon.IsVisibleAsync(),
                "Customer Management icon is not displayed.");

            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.AreEqual(
                "Drivers",
                (await PageHeading.InnerTextAsync()).Trim());

            await CustomersBreadcrumb.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.AreEqual(
                "Customers",
                (await CustomersBreadcrumb.InnerTextAsync()).Trim());

            await DriversBreadcrumb.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.AreEqual(
                "Drivers",
                (await DriversBreadcrumb.InnerTextAsync()).Trim());

            Console.WriteLine(
                "✔ Drivers page heading and breadcrumbs verified.");
        }


        // ============================================================
        // SELECT CUSTOMER
        // ============================================================

        public async Task SelectCustomerAsync()
        {
            await FleetDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await FleetDropdown.ClickAsync();

            var searchField =
                _page.Locator("input.select2-search__field").Last;

            await searchField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await searchField.FillAsync(
                "DO NOT DELETE(BP HO)");

            var customerOption =
                _page.Locator(
                    "ul.select2-results__options li").First;

            await customerOption.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await customerOption.ClickAsync();

            Console.WriteLine(
                "✔ Customer selected: DO NOT DELETE(BP HO)");
        }


        // ============================================================
        // SELECT STATUS
        // ============================================================

        public async Task SelectStatusAsync()
        {
            await StatusDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await StatusDropdown.ClickAsync();

            var searchField =
                _page.Locator("input.select2-search__field").Last;

            await searchField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await searchField.FillAsync("All");

            var statusOption =
                _page.Locator(
                    "ul.select2-results__options li").First;

            await statusOption.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await statusOption.ClickAsync();

            Console.WriteLine("✔ Status selected: All");
        }


        // ============================================================
        // VERIFY FORM FIELDS
        // ============================================================

        public async Task VerifyFormFieldsAsync()
        {
            await _page.Locator("#cphBody_txtSearch").WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await SearchButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await _page.Locator("#cphBody_btnExport").WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await ResetButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await AddDriverButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await StartDateField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await EndDateField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Console.WriteLine(
                "✔ Driver List form fields verified.");
        }


        // ============================================================
        // SEARCH
        // ============================================================

        public async Task SearchAsync()
        {
            await SearchButton.ClickAsync();

            await ShowEntriesDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Console.WriteLine(
                "✔ Search completed and Driver List table is displayed.");
        }


        // ============================================================
        // VERIFY SHOW ENTRIES
        // ============================================================

        public async Task VerifyShowEntriesAsync()
        {
            Assert.IsTrue(
                await ShowEntriesDropdown.IsVisibleAsync(),
                "Show Entries dropdown is not displayed.");

            Assert.IsTrue(
                await ShowEntriesDropdown.IsEnabledAsync(),
                "Show Entries dropdown is not enabled.");

            Console.WriteLine(
                "✔ Show Entries dropdown is displayed and enabled.");
        }


        // ============================================================
        // FILTER BY MOBILE NUMBER
        // ============================================================

        public async Task FilterByMobileNumberAsync()
        {
            await FilterTextField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await FilterTextField.FillAsync("DO NOT DELETE");

            Assert.IsTrue(
                await FilterTextField.IsVisibleAsync(),
                "Filter text field is not displayed.");

            Assert.IsTrue(
                await FilterTextField.IsEnabledAsync(),
                "Filter text field is not enabled.");

            Console.WriteLine(
                "✔ Filter text field is displayed and enabled.");

            Console.WriteLine(
                "✔ Driver list filtered by Driver Name: DO NOT DELETE");
        }


        // ============================================================
        // CLICK RESET PIN
        // ============================================================

        public async Task ClickResetPINAsync()
        {
            await ResetPINButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await ResetPINButton.ClickAsync();

            Console.WriteLine(
                "✔ Reset PIN button clicked.");
        }


        // ============================================================
        // VERIFY RESET PIN MODAL
        // ============================================================

        public async Task VerifyResetPINModalAsync()
        {
            await ResetPINModal.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.IsTrue(
                await ResetPINModal.IsVisibleAsync(),
                "Reset PIN modal is not displayed.");

            Console.WriteLine(
                "✔ Reset PIN modal is displayed.");

            await ResetPINModalTitle.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            string title =
                (await ResetPINModalTitle.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Are you sure you want to reset PIN for this Driver?",
                title,
                "Reset PIN modal title is incorrect.");

            Console.WriteLine(
                "✔ Reset PIN modal title verified.");
        }


        // ============================================================
        // VERIFY RESET PIN LABELS
        // ============================================================

        public async Task VerifyResetPINLabelsAsync()
        {
            var labels =
                _page.Locator("label.col-lg-3.control-label");

            string[] expectedLabels =
            {
                "Driver Name",
                "Cell Phone",
                "Card Number"
            };

            for (int i = 0; i < expectedLabels.Length; i++)
            {
                var label = labels.Nth(i);

                await label.WaitForAsync(
                    new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 10000
                    });

                string actualText =
                    (await label.InnerTextAsync()).Trim();

                Assert.AreEqual(
                    expectedLabels[i],
                    actualText,
                    $"Reset PIN modal label mismatch at position {i + 1}.");

                Console.WriteLine(
                    $"✔ Reset PIN label verified: {actualText}");
            }
        }


        // ============================================================
        // VERIFY DRIVER DETAILS
        // ============================================================

        public async Task VerifyDriverDetailsAsync()
        {
            await DriverNameLabel.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await CellPhoneLabel.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await CardNumberLabel.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            string driverName =
                (await DriverNameLabel.InnerTextAsync()).Trim();

            string cellPhone =
                (await CellPhoneLabel.InnerTextAsync()).Trim();

            string cardNumber =
                (await CardNumberLabel.InnerTextAsync()).Trim();

            Assert.IsTrue(
                driverName.Contains("DO NOT DELETE"),
                "The Driver Name value is incorrect.");

            Assert.IsTrue(
                cellPhone.Contains("27729053331"),
                "The Cell Phone value is incorrect.");

            Assert.IsTrue(
                cardNumber.Contains("500110000878"),
                "The Card Number value is incorrect.");

            Console.WriteLine(
                $"✔ Driver Name verified: {driverName}");

            Console.WriteLine(
                $"✔ Cell Phone verified: {cellPhone}");

            Console.WriteLine(
                $"✔ Card Number verified: {cardNumber}");
        }


        // ============================================================
        // VERIFY RESET PIN BUTTONS
        // ============================================================

        public async Task VerifyResetPINButtonsAsync()
        {
            await NoButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await YesButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await NoButton.IsVisibleAsync(),
                "No button is not displayed.");

            Assert.IsTrue(
                await YesButton.IsVisibleAsync(),
                "Yes button is not displayed.");

            Console.WriteLine(
                "✔ Reset PIN modal has No and Yes buttons.");
        }


        // ============================================================
        // CONFIRM RESET PIN
        // ============================================================

        public async Task ConfirmResetPINAsync()
        {
            await YesButton.ClickAsync();

            Console.WriteLine(
                "✔ Yes button clicked to reset PIN.");
        }


        // ============================================================
        // VERIFY SUCCESS MESSAGE
        // ============================================================

        public async Task VerifySuccessMessageAsync()
        {
            await SuccessMessage.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.IsTrue(
                await SuccessMessage.IsVisibleAsync(),
                "Success message is not displayed.");

            string message =
                (await SuccessMessage.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Driver PIN updated and PIN sent to the driver",
                message,
                "Reset PIN success message is incorrect.");

            Console.WriteLine(
                "✔ Reset PIN success message verified.");

            Console.WriteLine(
                $"Success Message: {message}");
        }
    }
}