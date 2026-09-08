using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class DriverMaintainPage
    {
        private readonly IPage _page;

        public DriverMaintainPage(IPage page)
        {
            _page = page;
        }

        // ============================================================
        // PAGE LOCATORS
        // ============================================================

        private ILocator CustomerManagementIcon =>
            _page.Locator("img[src='images/icons/customer_management_icon@2x.png']");

        private ILocator PageHeading =>
            _page.Locator("h2.heading");

        private ILocator CustomersBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn2");

        private ILocator DriversBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn3");

        private ILocator DriverManagementBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn4");

        // Wizard tabs
        private ILocator WizardTabs =>
            _page.Locator("a[data-toggle='tab']");

        private ILocator WizardLabels =>
            _page.Locator("small.wz-desc.box-block.text-semibold.step-label");

        // Details
        private ILocator CustomerAccountNumber =>
            _page.Locator("#cphBody_lblCustomerMobileNumber");

        private ILocator CardHolderStatusString =>
            _page.Locator("#cphBody_driverStatusLabel");

        private ILocator PreviousButton =>
            _page.Locator("#btn-Prev");

        private ILocator NextButton =>
            _page.Locator("#btn-next-fake");

        private ILocator CancelButton =>
            _page.Locator("#btn-Cancel");

        private ILocator SaveButton =>
            _page.Locator("#cphBody_btnSaveFake");

        private ILocator FirstNameRequiredMessage =>
            _page.Locator("#cphBody_reqtxtFirstName");

        private ILocator SurnameRequiredMessage =>
            _page.Locator("#cphBody_reqtxtSurName");

        // Details fields
        private ILocator DepartmentDropdown =>
            _page.Locator("#cphBody_selFleetDepartment");

        private ILocator DriverTypeDropdown =>
            _page.Locator("#cphBody_selDriverType");

        private ILocator DriverStatusDropdown =>
            _page.Locator("#cphBody_selDriverStatus");

        private ILocator CardNumberContainer =>
            _page.Locator("#select2-selCard-container");

        private ILocator PinField =>
            _page.Locator("#cphBody_txtPin");

        private ILocator SendPinCheckbox =>
            _page.Locator("#cphBody_chkSendPIN");

        private ILocator MobileNumberField =>
            _page.Locator("#cphBody_txtMobileNumber");

        private ILocator FleetManagerCheckbox =>
            _page.Locator("#cphBody_chkFleetManager");

        private ILocator AutomatedEonCheckbox =>
            _page.Locator("#cphBody_chkCanReceiveAutomatedEON");

        private ILocator FirstNameField =>
            _page.Locator("#cphBody_txtFirstName");

        private ILocator SurnameField =>
            _page.Locator("#cphBody_txtSurName");

        private ILocator EmailField =>
            _page.Locator("#cphBody_txtEmail");

        private ILocator VerifyVehicleCheckbox =>
            _page.Locator("#cphBody_chkVerifyVehicle");

        private ILocator ExpiryDateField =>
            _page.Locator("#cphBody_txtExpiryDate");

        // Other Information
        private ILocator DriverReferenceField =>
            _page.Locator("#cphBody_txtDriverReference");

        private ILocator LicenseCodeField =>
            _page.Locator("#cphBody_txtLicenseCode");

        private ILocator LicenseNumberField =>
            _page.Locator("#cphBody_txtLicense");

        private ILocator LicenseExpiryField =>
            _page.Locator("#cphBody_txtLicenseExpiry");

        private ILocator LicenseExpiryCalendar =>
            _page.Locator("#cphBody_calLicenseExpiry_popupDiv");

        private ILocator LicenseExpiryCalendarTitle =>
            _page.Locator("#cphBody_calLicenseExpiry_title");

        private ILocator LicenseExpiryNextArrow =>
            _page.Locator("#cphBody_calLicenseExpiry_nextArrow");

        private ILocator LicenseExpiryYear =>
            _page.Locator("#cphBody_calLicenseExpiry_year_1_0");

        private ILocator LicenseExpiryMonth =>
            _page.Locator("#cphBody_calLicenseExpiry_month_0_1");

        private ILocator LicenseExpiryDay =>
            _page.Locator("#cphBody_calLicenseExpiry_day_1_0");

        private ILocator PdpExpiryField =>
            _page.Locator("#cphBody_txtPDPExpiry");

        private ILocator PdpExpiryCalendar =>
            _page.Locator("#cphBody_calPDPExpiry_popupDiv");

        private ILocator PdpExpiryCalendarTitle =>
            _page.Locator("#cphBody_calPDPExpiry_title");

        private ILocator PdpExpiryNextArrow =>
            _page.Locator("#cphBody_calPDPExpiry_nextArrow");

        private ILocator PdpExpiryYear =>
            _page.Locator("#cphBody_calPDPExpiry_year_1_0");

        private ILocator PdpExpiryMonth =>
            _page.Locator("#cphBody_calPDPExpiry_month_0_1");

        private ILocator PdpExpiryDay =>
            _page.Locator("#cphBody_calPDPExpiry_day_1_0");

        private ILocator NationalIdField =>
            _page.Locator("#cphBody_txtIdNumber");

        private ILocator PassportNumberField =>
            _page.Locator("#cphBody_txtPassportNumber");

        private ILocator DefaultAuthorisedAmountField =>
            _page.Locator("#cphBody_txtDefaultAuthorisedAmount");

        // Allowed Locations
        private ILocator VerifyLocationCheckbox =>
            _page.Locator("#cphBody_chkVerifyLocation");

        // Filling Rules
        private ILocator AmountPerTransaction =>
            _page.Locator("#cphBody_txtAmountPerTransaction");

        private ILocator AmountPerDay =>
            _page.Locator("#cphBody_txtAmountPerDay");

        private ILocator AmountPerWeek =>
            _page.Locator("#cphBody_txtAmountPerWeek");

        private ILocator AmountPerMonth =>
            _page.Locator("#cphBody_txtAmountPerMonth");

        private ILocator VolumePerTransaction =>
            _page.Locator("#cphBody_txtVolumePerTransaction");

        private ILocator VolumePerDay =>
            _page.Locator("#cphBody_txtVolumePerDay");

        private ILocator VolumePerWeek =>
            _page.Locator("#cphBody_txtVolumePerWeek");

        private ILocator VolumePerMonth =>
            _page.Locator("#cphBody_txtVolumePerMonth");

        private ILocator TransactionPerDay =>
            _page.Locator("#cphBody_txtTransactionPerDay");

        private ILocator TransactionPerWeek =>
            _page.Locator("#cphBody_txtTransactionPerWeek");

        private ILocator TransactionPerMonth =>
            _page.Locator("#cphBody_txtTransactionPerMonth");

        private ILocator IncorrectPinAttempts =>
            _page.Locator("#cphBody_txtNumberOfIncorrectPin");

        private ILocator LockoutAttempts =>
            _page.Locator("#cphBody_txtCardLockOutAttempts");

        private ILocator LockoutPeriod =>
            _page.Locator("#cphBody_txtCardLockOutPeriod");

        private ILocator VolumePerTransactionMeasurement =>
            _page.Locator("#cphBody_litresSpan3");

        private ILocator VolumePerDayMeasurement =>
            _page.Locator("#cphBody_litresSpan4");

        private ILocator VolumePerWeekMeasurement =>
            _page.Locator("#cphBody_litresSpan5");

        private ILocator VolumePerMonthMeasurement =>
            _page.Locator("#cphBody_litresSpan6");

        // Audit Trail
        private ILocator SuccessMessage =>
            _page.Locator("p.alert-message");

        // ============================================================
        // NAVIGATION
        // ============================================================

        public async Task NavigateToDriverListAsync()
        {
            await _page.GotoAsync(
                "https://admin-stage.payment24.co/DriverList.aspx",
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded
                });

            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public async Task SelectFleetAsync(string fleetName)
        {
            // Open Select2 fleet dropdown
            await _page.Locator("#select2-ddlFleet-container").ClickAsync();

            var searchField = _page.Locator(
                "input.select2-search__field:visible"
            ).Last;

            await searchField.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await searchField.FillAsync(fleetName);

            var fleetOption = _page.Locator(
                 "ul.select2-results__options li",
                 new LocatorOptions
                 {
                     HasTextString = fleetName
                 });

            await fleetOption.First.ClickAsync();

            await _page.WaitForTimeoutAsync(1000);
        }

        public async Task ClickAddDriverAsync()
        {
            await _page.Locator("#cphBody_btnAddDriver").ClickAsync();

            await PageHeading.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible
            });

            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        // ============================================================
        // PAGE VERIFICATION
        // ============================================================

        public async Task VerifyDriverManagementPageAsync()
        {
            Assert.IsTrue(
                await CustomerManagementIcon.IsVisibleAsync(),
                "Customer Management icon is not visible.");

            Assert.AreEqual(
                "Driver Management",
                await PageHeading.InnerTextAsync());

            Assert.AreEqual(
                "Customers",
                await CustomersBreadcrumb.InnerTextAsync());

            Assert.AreEqual(
                "Drivers",
                await DriversBreadcrumb.InnerTextAsync());

            Assert.AreEqual(
                "Driver Management",
                await DriverManagementBreadcrumb.InnerTextAsync());

            Console.WriteLine("✔ Driver Management page verified.");
        }

        // ============================================================
        // DETAILS TAB
        // ============================================================

        public async Task VerifyDetailsTabAsync()
        {
            await WizardTabs.Nth(0).ClickAsync();

            Assert.AreEqual(
                "Details",
                await WizardLabels.Nth(0).InnerTextAsync());

            Console.WriteLine("✔ Details tab verified.");

            string[] expectedLabels =
            {
                "Customer Details",
                "Account Number",
                "Department",
                "Card Holder Type",
                //"Card Holder Status",
                "Card Number",
                "Pin",
                //"Send PIN via SMS", Changed to "Send Driver Pin"
                "Send Driver Pin",
                "Mobile Number",
                "Manager",
                "Receive Automated EON",
                "First Name",
                "Surname",
                "Country of Residency",
                "Country of Birth",
                "Email",
                "Verify Vehicle",
                "Expiry Date"
            };

            var labels = _page.Locator(
                "label.col-lg-2.control-label");

            for (int i = 0; i < expectedLabels.Length; i++)
            {
                Assert.AreEqual(
                    expectedLabels[i],
                    await labels.Nth(i).InnerTextAsync(),
                    $"Details label mismatch at position {i + 1}.");
            }

            Console.WriteLine("✔ Details fields verified.");

            Assert.AreEqual(
                "Card Holder Status",
                await CardHolderStatusString.InnerTextAsync());

            Assert.AreEqual(
                "BPCRT75764",
                await CustomerAccountNumber.InnerTextAsync());

            Console.WriteLine("✔ Customer account number verified.");
        }

        public async Task VerifyNavigationButtonsAsync()
        {
            Assert.IsTrue(
                await PreviousButton.IsVisibleAsync(),
                "Previous button is not visible.");

            string previousClass =
                await PreviousButton.GetAttributeAsync("class") ?? "";

            Assert.IsTrue(
                previousClass.Contains("disabled"),
                "Previous button is not disabled.");

            Assert.IsTrue(
                await NextButton.IsVisibleAsync(),
                "Next button is not visible.");

            Assert.IsTrue(
                await CancelButton.IsVisibleAsync(),
                "Cancel button is not visible.");

            Console.WriteLine("✔ Previous, Next and Cancel buttons verified.");
        }

        public async Task VerifyRequiredValidationAsync()
        {
            await SaveButton.ClickAsync();

            await FirstNameRequiredMessage.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible
                });

            Assert.AreEqual(
                "* Required",
                await FirstNameRequiredMessage.InnerTextAsync());

            Assert.AreEqual(
                "* Required",
                await SurnameRequiredMessage.InnerTextAsync());

            Console.WriteLine("✔ Required field validation verified.");
        }

        public async Task FillDetailsAsync()
        {
            string date = DateTime.Now.ToString("ddMMyy" + "HH:mm:ss");
            // Department
            await DepartmentDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = "Default"
                });

            // Card holder type
            await DriverTypeDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Value = "1"
                });

            // Card holder status
            await DriverStatusDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Value = "1"
                });

            // Card number Select2
            await CardNumberContainer.ClickAsync();

            var cardSearch =
                _page.Locator("input.select2-search__field:visible").Last;

            await cardSearch.FillAsync("500110");

            var cardOption =
                _page.Locator("ul.select2-results__options li")
                     .Filter(new LocatorFilterOptions
                     {
                         HasTextString = "500110"
                     });

            if (await cardOption.CountAsync() == 0)
            {
                cardOption =
                    _page.Locator("ul.select2-results__options li").First;
            }

            await cardOption.ClickAsync();

            // PIN
            var pin = new Random().Next(2563, 9999);

            await PinField.FillAsync(pin.ToString());

            // Send PIN
            await SendPinCheckbox.CheckAsync();

            // Mobile
            await MobileNumberField.FillAsync("729053331");

            // Manager
            await FleetManagerCheckbox.CheckAsync();

            // Automated EON
            await AutomatedEonCheckbox.CheckAsync();

            // First name
            await FirstNameField.FillAsync($"QA{date}");

            // Surname
            await SurnameField.FillAsync($"Payment{date}");

            // Email
            await EmailField.FillAsync("QA@payment24.com");

            // Verify Vehicle intentionally left unchanged
            // Expiry Date intentionally left as default

            Console.WriteLine("✔ Details data entered.");
        }

        // ============================================================
        // OTHER INFORMATION
        // ============================================================


        public async Task OpenOtherInformationAsync()
        {
            await WizardTabs.Nth(1).ClickAsync();

            Assert.AreEqual(
                "Other Information",
                await WizardLabels.Nth(1).InnerTextAsync());

            // Wait for the Other Information tab/panel to become active.
            var activeTabPanel = _page.Locator(
                ".tab-pane.active, .tab-content > .active"
            ).Last;

            await activeTabPanel.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible
                });

            Console.WriteLine("✔ Other Information tab verified.");
        }


        public async Task VerifyOtherInformationFieldsAsync()
        {
            Console.WriteLine("");
            Console.WriteLine("==========================================");
            Console.WriteLine("VERIFYING OTHER INFORMATION FIELDS");
            Console.WriteLine("==========================================");

            // ============================================================
            // DRIVER REFERENCE & DRIVER NUMBER
            // These use span.col-lg-2.control-label
            // ============================================================

            string[] spanLabels =
            {
        "Driver Reference",
        "Driver Number"
    };

            foreach (string expectedLabel in spanLabels)
            {
                var labels = _page.Locator(
                    "span.col-lg-2.control-label"
                ).Filter(
                    new LocatorFilterOptions
                    {
                        HasTextString = expectedLabel
                    });

                int count = await labels.CountAsync();

                Assert.IsTrue(
                    count > 0,
                    $"Other Information field '{expectedLabel}' was not found.");

                bool visibleFound = false;

                for (int i = 0; i < count; i++)
                {
                    var currentLabel = labels.Nth(i);

                    if (await currentLabel.IsVisibleAsync())
                    {
                        string actualText =
                            (await currentLabel.InnerTextAsync()).Trim();

                        Assert.AreEqual(
                            expectedLabel,
                            actualText,
                            $"Incorrect label. Expected '{expectedLabel}' but found '{actualText}'.");

                        Console.WriteLine(
                            $"✔ {expectedLabel} verified.");

                        visibleFound = true;
                        break;
                    }
                }

                Assert.IsTrue(
                    visibleFound,
                    $"Other Information field '{expectedLabel}' exists but no visible instance was found.");
            }

            // ============================================================
            // REMAINING OTHER INFORMATION FIELDS
            // These use label.col-lg-2.control-label
            // ============================================================

            string[] labelFields =
            {
        "License Code",
        "License Number",
        "License Expiry",
        "PDP Expiry",
        "National ID Number",
        "Passport Number",
        "Employee Number",
        "Default Authorisation Amount"
    };

            foreach (string expectedLabel in labelFields)
            {
                var labels = _page.Locator(
                    "label.col-lg-2.control-label"
                ).Filter(
                    new LocatorFilterOptions
                    {
                        HasTextString = expectedLabel
                    });

                int count = await labels.CountAsync();

                Assert.IsTrue(
                    count > 0,
                    $"Other Information field '{expectedLabel}' was not found.");

                bool visibleFound = false;

                for (int i = 0; i < count; i++)
                {
                    var currentLabel = labels.Nth(i);

                    if (await currentLabel.IsVisibleAsync())
                    {
                        string actualText =
                            (await currentLabel.InnerTextAsync()).Trim();

                        Assert.AreEqual(
                            expectedLabel,
                            actualText,
                            $"Incorrect label. Expected '{expectedLabel}' but found '{actualText}'.");

                        Console.WriteLine(
                            $"✔ {expectedLabel} verified.");

                        visibleFound = true;
                        break;
                    }
                }

                Assert.IsTrue(
                    visibleFound,
                    $"Other Information field '{expectedLabel}' exists but no visible instance was found.");
            }

            Console.WriteLine("");
            Console.WriteLine(
                "✔ All Other Information fields verified successfully.");
            Console.WriteLine("==========================================");
        }



        public async Task FillOtherInformationAsync()
        {
            string date = DateTime.Today.ToString("ddMMyyyy");

            await DriverReferenceField.FillAsync($"QA{date}");

            await LicenseCodeField.FillAsync("10");

            await LicenseNumberField.FillAsync($"LN{date}");

            await SelectLicenseExpiryDateAsync();

            await SelectPdpExpiryDateAsync();

            // National ID intentionally left blank

            await PassportNumberField.FillAsync($"TEST{date}");

            await DefaultAuthorisedAmountField.FillAsync("1000");

            Console.WriteLine("✔ Other Information data entered.");
        }


        // ============================================================
        // DATE PICKERS
        // ============================================================

        private async Task SelectLicenseExpiryDateAsync()
        {
            Console.WriteLine("Selecting License Expiry date...");

            // ========================================================
            // OPEN CALENDAR
            // ========================================================

            await LicenseExpiryField.ClickAsync();

            await LicenseExpiryCalendar.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            // ========================================================
            // OPEN MONTH VIEW
            // ========================================================

            await LicenseExpiryCalendarTitle.ClickAsync();

            var monthsBody = _page.Locator(
                "#cphBody_calLicenseExpiry_monthsBody");

            await monthsBody.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Console.WriteLine("✔ Calendar month view opened.");

            // ========================================================
            // OPEN YEAR VIEW
            // ========================================================

            await LicenseExpiryCalendarTitle.ClickAsync();

            var yearsBody = _page.Locator(
                "#cphBody_calLicenseExpiry_yearsBody");

            await yearsBody.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Console.WriteLine("✔ Calendar year view opened.");

            // ========================================================
            // SELECT YEAR
            // ========================================================

            var visibleYear = yearsBody
                .Locator(".ajax__calendar_year:visible")
                .First;

            await visibleYear.ClickAsync();

            Console.WriteLine("✔ License expiry year selected.");

            // ========================================================
            // SELECT MONTH
            // ========================================================

            await monthsBody.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var visibleMonth = monthsBody
                .Locator(".ajax__calendar_month:visible")
                .First;

            await visibleMonth.ClickAsync();

            Console.WriteLine("✔ License expiry month selected.");

            // ========================================================
            // SELECT DAY
            // ========================================================

            var daysBody = _page.Locator(
                "#cphBody_calLicenseExpiry_days");

            await daysBody.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var visibleDay = daysBody
                .Locator(".ajax__calendar_day:visible")
                .First;

            await visibleDay.ClickAsync();

            Console.WriteLine("✔ License Expiry date selected.");
        }

        private async Task SelectPdpExpiryDateAsync()
        {
            Console.WriteLine("Selecting PDP Expiry date...");

            // =========================================================
            // 1. OPEN PDP EXPIRY CALENDAR
            // =========================================================
            await PdpExpiryField.ClickAsync();

            await PdpExpiryCalendar.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Console.WriteLine("✔ PDP calendar opened.");

            await _page.WaitForTimeoutAsync(500);

            // =========================================================
            // 2. CHECK MONTH VIEW
            // =========================================================
            var monthsBody = _page.Locator(
                "#cphBody_calPDPExpiry_monthsBody");

            if (!await monthsBody.IsVisibleAsync())
            {
                Console.WriteLine("PDP calendar is not in month view.");

                await PdpExpiryCalendarTitle.WaitForAsync(
                    new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 5000
                    });

                await PdpExpiryCalendarTitle.ClickAsync(
                    new LocatorClickOptions
                    {
                        Force = true
                    });

                await _page.WaitForTimeoutAsync(500);
            }

            // =========================================================
            // 3. WAIT FOR MONTH VIEW
            // =========================================================
            await monthsBody.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Console.WriteLine("✔ PDP calendar month view opened.");

            // =========================================================
            // 4. SELECT CURRENT MONTH
            // =========================================================
            int monthIndex = DateTime.Today.Month - 1;

            var month = monthsBody
                .Locator(".ajax__calendar_month")
                .Nth(monthIndex);

            await month.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 5000
                });

            await month.ClickAsync();

            Console.WriteLine(
                $"✔ PDP expiry month selected: {DateTime.Today:MMMM}");

            // =========================================================
            // 5. WAIT FOR DAY VIEW
            // =========================================================
            var daysBody = _page.Locator(
                "#cphBody_calPDPExpiry_days");

            await daysBody.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Console.WriteLine("✔ PDP calendar day view opened.");

            // =========================================================
            // 6. SELECT TODAY'S DAY
            // =========================================================
            int dayNumber = DateTime.Today.Day;

            var day = daysBody
                .Locator(".ajax__calendar_day")
                .Filter(new LocatorFilterOptions
                {
                    HasText = dayNumber.ToString()
                })
                .First;

            await day.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 5000
                });

            await day.ClickAsync();

            Console.WriteLine(
                $"✔ PDP expiry day selected: {dayNumber}");

            // =========================================================
            // 7. VERIFY FIELD WAS POPULATED
            // =========================================================
            await _page.WaitForTimeoutAsync(300);

            string selectedDate = await PdpExpiryField.InputValueAsync();

            Console.WriteLine(
                $"✔ PDP Expiry field value: {selectedDate}");

            Assert.IsFalse(
                string.IsNullOrWhiteSpace(selectedDate),
                "PDP Expiry date was not populated.");

            Console.WriteLine(
                "✔ PDP Expiry date selected successfully.");
        }


        // ============================================================
        // FILLING RULES AND LIMITS
        // ============================================================

        public async Task OpenFillingRulesAndLimitsAsync()
        {
            await WizardTabs.Nth(3).ClickAsync();

            Assert.AreEqual(
                "Filling Rules and Limits",
                await WizardLabels.Nth(3).InnerTextAsync());

            Console.WriteLine("✔ Filling Rules and Limits tab verified.");
        }

        public async Task VerifyFillingRulesAndLimitsFieldsAsync()
        {
            string[] expectedCol4Labels =
            {
        "Temporary Disable Filling Rules",
        "Start Time Hour",
        "End Time Hour",
        "Select days of the week",
        "Fueling Criteria",
        "Volume Per Transaction",
        "Volume Per Day",
        "Volume Per Week",
        "Volume Per Month",
        "Transaction Per Day",
        "Transaction Per Week",
        "Transaction Per Month",
        "Number Of Incorrect Pin Entry",
        "No. of Attempts for Lock out",
        "Card Lock out Period Minute",
        "Velocity Minutes",
        "Allow All Vehicles"
    };

            // Only get labels that are currently visible.
            var labels4 =
                _page.Locator("label.col-lg-4.control-label:visible");

            int labels4Count = await labels4.CountAsync();

            Console.WriteLine(
                $"Visible col-lg-4 labels found: {labels4Count}");

            for (int i = 0; i < expectedCol4Labels.Length; i++)
            {
                string actualLabel =
                    (await labels4.Nth(i).InnerTextAsync()).Trim();

                Console.WriteLine(
                    $"Filling Rules [{i + 1}] Expected: '{expectedCol4Labels[i]}' | Actual: '{actualLabel}'");

                Assert.AreEqual(
                    expectedCol4Labels[i],
                    actualLabel,
                    $"Filling Rules label mismatch at position {i + 1}.");
            }

            string[] expectedCol3Labels =
            {
        "Amount Per Transaction",
        "Amount Per Day",
        "Amount Per Week",
        "Amount Per Month"
    };

            // Only get visible amount labels.
            var labels3 =
                _page.Locator("label.col-lg-3.control-label:visible");

            int labels3Count = await labels3.CountAsync();

            Console.WriteLine(
                $"Visible col-lg-3 labels found: {labels3Count}");

            for (int i = 0; i < expectedCol3Labels.Length; i++)
            {
                string actualLabel =
                    (await labels3.Nth(i).InnerTextAsync()).Trim();

                Console.WriteLine(
                    $"Amount [{i + 1}] Expected: '{expectedCol3Labels[i]}' | Actual: '{actualLabel}'");

                Assert.AreEqual(
                    expectedCol3Labels[i],
                    actualLabel,
                    $"Amount label mismatch at position {i + 1}.");
            }

            Console.WriteLine(
                "✔ Filling Rules and Limits fields verified.");
        }

        public async Task VerifyCurrencyAndMeasurementsAsync()
        {
            var currency =
                _page.Locator("span.col-lg-1").First;

            Assert.AreEqual(
                "(R)",
                await currency.InnerTextAsync());

            Assert.AreEqual(
                "litres",
                await VolumePerTransactionMeasurement.InnerTextAsync());

            Assert.AreEqual(
                "litres",
                await VolumePerDayMeasurement.InnerTextAsync());

            Assert.AreEqual(
                "litres",
                await VolumePerWeekMeasurement.InnerTextAsync());

            Assert.AreEqual(
                "litres",
                await VolumePerMonthMeasurement.InnerTextAsync());

            Console.WriteLine("✔ Currency and measurements verified.");
        }


        // ============================================================
        // ALLOWED LOCATIONS
        // ============================================================

        public async Task OpenAllowedLocationsAsync()
        {
            await WizardTabs.Nth(2).ClickAsync();

            Assert.AreEqual(
                "Allowed Locations",
                await WizardLabels.Nth(2).InnerTextAsync());

            Console.WriteLine("✔ Allowed Locations tab verified.");
        }

        public async Task VerifyAllowedLocationsFieldsAsync()
        {
            string[] expectedLabels =
            {
                "Verify Location",
                "Merchant",
                "Merchant Cluster"
            };

            var labels =
                _page.Locator("label.col-lg-2.control-label");

            for (int i = 0; i < expectedLabels.Length; i++)
            {
                Assert.AreEqual(
                    expectedLabels[i],
                    await labels.Nth(i + 26).InnerTextAsync(),
                    $"Allowed Locations label mismatch at position {i + 1}.");
            }

            Console.WriteLine("✔ Allowed Locations fields verified.");
        }

        public async Task FillAllowedLocationsAsync()
        {
            // Verify Location intentionally NOT selected
            // because the original Selenium test states this redirects
            // back to the login page.

            Assert.IsTrue(
                await VerifyLocationCheckbox.IsVisibleAsync(),
                "Verify Location checkbox is not visible.");

            // Merchant Select2
            var select2Fields =
                _page.Locator("input.select2-search__field:visible");

            if (await select2Fields.CountAsync() > 0)
            {
                await select2Fields.First.ClickAsync();

                var merchantOption =
                    _page.Locator(
                        "ul.select2-results__options li").First;

                await merchantOption.ClickAsync();
            }

            // Merchant Cluster
            var visibleSearchFields =
                _page.Locator("input.select2-search__field:visible");

            if (await visibleSearchFields.CountAsync() > 0)
            {
                await visibleSearchFields.Last.FillAsync("Default");

                var clusterOption =
                    _page.Locator(
                        "ul.select2-results__options li").First;

                await clusterOption.ClickAsync();
            }

            Console.WriteLine("✔ Allowed Locations data entered.");
        }

        // ============================================================
        // AUDIT TRAIL
        // ============================================================

        public async Task OpenAuditTrailAsync()
        {
            await WizardTabs.Nth(4).ClickAsync();

            Assert.AreEqual(
                "Audit Trail",
                await WizardLabels.Nth(4).InnerTextAsync());

            Console.WriteLine("✔ Audit Trail tab verified.");
        }

        public async Task VerifyAuditTrailFieldsAsync()
        {
            string[] expectedLabels =
            {
                "Created By",
                "Created Date",
                "Last Modified By",
                "Last Modified Date"
            };

            var labels =
                _page.Locator("label.col-lg-2.control-label");

            for (int i = 0; i < expectedLabels.Length; i++)
            {
                Assert.AreEqual(
                    expectedLabels[i],
                    await labels.Nth(i + 29).InnerTextAsync(),
                    $"Audit Trail label mismatch at position {i + 1}.");
            }

            Console.WriteLine("✔ Audit Trail fields verified.");
        }

        // ============================================================
        // SAVE
        // ============================================================

        public async Task SaveDriverAsync()
        {
            Console.WriteLine("Saving driver...");

            await SaveButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await SaveButton.IsEnabledAsync(),
                "Save button is disabled.");

            await SaveButton.ClickAsync();

            // Allow the WebForms/AJAX save operation to complete
            await _page.WaitForTimeoutAsync(2000);

            Console.WriteLine($"Current URL: {_page.Url}");

            // =========================================================
            // CHECK ALL ALERT/MESSAGE ELEMENTS
            // =========================================================

            var alerts = _page.Locator(
                ".alert-message, .alert, .message, [class*='message']");

            int alertCount = await alerts.CountAsync();

            Console.WriteLine(
                $"Message elements found: {alertCount}");

            for (int i = 0; i < alertCount; i++)
            {
                string text = (await alerts.Nth(i).InnerTextAsync()).Trim();

                Console.WriteLine(
                    $"Message [{i + 1}]: '{text}' | " +
                    $"Visible: {await alerts.Nth(i).IsVisibleAsync()}");
            }

            // =========================================================
            // LOOK FOR SUCCESS TEXT ANYWHERE ON PAGE
            // =========================================================

            var successText = _page.GetByText(
                "Driver has been updated successfully.",
                new PageGetByTextOptions
                {
                    Exact = true
                });

            int successCount = await successText.CountAsync();

            Console.WriteLine(
                $"Success text elements found: {successCount}");

            // =========================================================
            // FAIL TEST IF SUCCESS MESSAGE DOES NOT EXIST
            // =========================================================

            Assert.IsTrue(
                successCount > 0,
                "Driver was saved, but 'Driver has been updated successfully.' was not found on the page.");

            Assert.IsTrue(
                await successText.First.IsVisibleAsync(),
                "Driver success message exists but is not visible.");

            Console.WriteLine(
                "✔ Driver has been updated successfully.");
        }



        internal class LocatorOptions : PageLocatorOptions
        {
            public string HasTextString { get; set; }
        }

    }
}
