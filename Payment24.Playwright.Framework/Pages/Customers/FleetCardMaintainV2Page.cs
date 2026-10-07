using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class FleetCardMaintainV2Page
    {
        private readonly IPage _page;

        public FleetCardMaintainV2Page(IPage page)
        {
            _page = page;
        }

        // ============================================================
        // PAGE / VEHICLE LIST
        // ============================================================

        private ILocator PageHeading =>
            _page.Locator("h2.heading");

        private ILocator FleetDropdown =>
            _page.Locator("#select2-ddlFleet-container");

        private ILocator FleetSearchField =>
            _page.Locator("input.select2-search__field");

        private ILocator FleetResults =>
            _page.Locator("#select2-ddlFleet-results li").First;

        private ILocator AddVehicleButton =>
            _page.Locator("#cphBody_btnAdd");


        // ============================================================
        // VEHICLE DETAILS - TAB 1
        // ============================================================

        private ILocator VehicleDetailsTab =>
            _page.Locator("a[data-toggle='tab']").Nth(0);

        private ILocator VehicleDetailsTabText =>
            _page.Locator("small.wz-desc.box-block.text-semibold").Nth(0);

        private ILocator CustomerValue =>
            _page.Locator("#select2-cphBody_selFleet-container");

        private ILocator AccountNumberValue =>
            _page.Locator("#cphBody_lblCustomerMobileNumber");

        private ILocator TankSizeField =>
            _page.Locator("#cphBody_txtTankSize");

        private ILocator DepartmentDropdown =>
            _page.Locator("#select2-cphBody_selFleetDepartment-container");

        private ILocator DepartmentSearchField =>
            _page.Locator("input.select2-search__field");

        private ILocator DepartmentResults =>
            _page.Locator(
                "#select2-cphBody_selFleetDepartment-results li").First;

        private ILocator ChargeCardFeeCheckbox =>
            _page.Locator("#cphBody_cbChargeCardFee");

        private ILocator VehicleTagDropdown =>
            _page.Locator("#select2-cphBody_selCard-container");

        private ILocator VehicleTagResults =>
            _page.Locator("#select2-cphBody_selCard-results li").Nth(1);

        private ILocator VehicleStatusDropdown =>
            _page.Locator("#cphBody_selFleetCardStatus");

        private ILocator RegistrationNumberField =>
            _page.Locator("#cphBody_txtRegistrationNumber");

        private ILocator VehicleTypeDropdown =>
            _page.Locator("#cphBody_selVehicleType");

        private ILocator VehicleCategoryDropdown =>
            _page.Locator("#cphBody_selFleetCardCategory");

        private ILocator VehicleMakeDropdown =>
            _page.Locator("#select2-cphBody_selMake-container");

        private ILocator VehicleMakeSearchField =>
            _page.Locator("input.select2-search__field");

        private ILocator VehicleMakeResults =>
            _page.Locator("ul.select2-results__options li").First;

        private ILocator VehicleColourField =>
            _page.Locator("#cphBody_txtColour");

        private ILocator YearModelDropdown =>
            _page.Locator("#cphBody_selYearModel");

        private ILocator FuelTypeDropdown =>
            _page.Locator("#cphBody_selMerchantProduct");

        private ILocator IssueDateField =>
            _page.Locator("#cphBody_txtIssueDate");

        private ILocator IssueDateHeader =>
            _page.Locator("#cphBody_calIssueDate_header");

        private ILocator IssueDateTitle =>
            _page.Locator("#cphBody_calIssueDate_title");

        private ILocator IssueDateYear =>
            _page.Locator("#cphBody_calIssueDate_year_1_0");

        private ILocator IssueDateMonth =>
            _page.Locator("#cphBody_calIssueDate_month_0_0");

        private ILocator IssueDateDay =>
            _page.Locator("#cphBody_calIssueDate_day_1_0");


        private ILocator ExpiryDateCalendarButton =>
             _page.Locator("#cphBody_calExpiryDate_header");
        private ILocator ExpiryDateField =>
            _page.Locator("#cphBody_txtExpiryDate");

        private ILocator ExpiryDateHeader =>
            _page.Locator("#cphBody_calExpiryDate_header");

        private ILocator ExpiryDateTitle =>
            _page.Locator("#cphBody_calExpiryDate_title");

        private ILocator ExpiryDateYear =>
            _page.Locator("#cphBody_calExpiryDate_year_1_0");

        private ILocator ExpiryDateMonth =>
            _page.Locator("#cphBody_calExpiryDate_month_0_1");

        private ILocator ExpiryDateDay =>
            _page.Locator("#cphBody_calExpiryDate_day_1_0");

        private ILocator ReferenceNumberField =>
            _page.Locator("#cphBody_txtReferenceNumber");

        private ILocator OfferDropdown =>
            _page.Locator("#cphBody_selOffer");


        // ============================================================
        // TAB 2 - VEHICLE MAINTENANCE
        // ============================================================

        private ILocator VehicleMaintenanceTab =>
            _page.Locator("a[data-toggle='tab']").Nth(1);

        private ILocator VehicleMaintenanceTabText =>
            _page.Locator("small.wz-desc.box-block.text-semibold").Nth(1);

        private ILocator CaptureOdoCheckbox =>
            _page.Locator("#cphBody_chkCaptureOD");

        private ILocator EnableTelematicsCheckbox =>
            _page.Locator("#cphBody_chkEnableTelematics");

        private ILocator CurrentOdoField =>
            _page.Locator("#cphBody_txtODOMeter");

        private ILocator ExpectedFuelConsumptionField =>
            _page.Locator("#cphBody_txtAveragePerLitre");

        private ILocator ActualFuelConsumptionField =>
            _page.Locator("#cphBody_txtCurrentAveragePerLitre");

        private ILocator ServiceIntervalField =>
            _page.Locator("#cphBody_txtServiceInterval");

        private ILocator LastServiceOdoField =>
            _page.Locator("#cphBody_txtLastServiceODO");

        private ILocator NotificationOdoField =>
            _page.Locator("#cphBody_txtNotificationODOReading");

        private ILocator LastServiceDateField =>
            _page.Locator("#cphBody_txtLastServiceDate");

        private ILocator NextServiceOdoField =>
            _page.Locator("#cphBody_txtNextServiceODO");

        private ILocator ServiceCycleDropdown =>
            _page.Locator("#cphBody_selServiceCycle");

        private ILocator NextOilChangeDateField =>
            _page.Locator("#cphBody_txtNextOilDate");

        private ILocator NextTyreChangeDateField =>
            _page.Locator("#cphBody_txtNextTyreChangeDate");

        private ILocator NextBatteryChangeDateField =>
            _page.Locator("#cphBody_txtNextBatteryChangeDate");

        private ILocator NextOilChangeOdoField =>
            _page.Locator("#cphBody_txtNextOilODO");

        private ILocator TyreWidthField =>
            _page.Locator("#cphBody_txtTyreWidth");

        private ILocator TyreRatioField =>
            _page.Locator("#cphBody_txtTyreRatio");

        private ILocator WheelDiameterField =>
            _page.Locator("#cphBody_txtWheelDiameter");

        private ILocator VehicleLicenseExpiryField =>
            _page.Locator("#cphBody_txtVehicleLicenseExpiry");

        private ILocator VinNumberField =>
            _page.Locator("#cphBody_txtVinNumber");

        private ILocator EngineNumberField =>
            _page.Locator("#cphBody_txtEngineNumber");

        private ILocator InsurancePolicyNumberField =>
            _page.Locator("#cphBody_txtInsurancePolicyNumber");

        private ILocator InsuranceCompanyNameField =>
            _page.Locator("#cphBody_txtInsuranceCompanyName");

        private ILocator NextInsuranceReviewField =>
            _page.Locator("#cphBody_txtNextInsuranceReview");


        // ============================================================
        // TAB 3 - FILLING RULES AND LIMITS
        // ============================================================

        private ILocator FillingRulesTab =>
            _page.Locator("a[data-toggle='tab']").Nth(2);

        private ILocator FillingRulesTabText =>
            _page.Locator("small.wz-desc.box-block.text-semibold").Nth(2);

        private ILocator AuthorizeOverrideCheckbox =>
            _page.Locator("#cphBody_chkAuthorizeOverride");

        private ILocator ReasonForOverrideField =>
            _page.Locator("#cphBody_txtReasonForOveride");

        private ILocator AccountBalance =>
            _page.Locator("#cphBody_lblAccountBalance");

        private ILocator AmountRadioButton =>
            _page.Locator("#cphBody_ChkbxFillingCriteriaAmount");

        private ILocator VolumeRadioButton =>
            _page.Locator("#cphBody_ChkbxFillingCriteriaVolume");

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

        private ILocator OilLimit =>
            _page.Locator("#cphBody_txtOilLimit");

        private ILocator IncorrectPinAttempts =>
            _page.Locator("#cphBody_txtNumberOfIncorrectPin");

        private ILocator CardLockOutAttempts =>
            _page.Locator("#cphBody_txtCardLockOutAttempts");

        private ILocator CardLockOutPeriod =>
            _page.Locator("#cphBody_txtCardLockOutPeriod");

        private ILocator Velocity =>
            _page.Locator("#cphBody_txtVelocity");

        private ILocator MinKmBetweenTransaction =>
            _page.Locator("#cphBody_txtKMBetweenTransaction");


        // ============================================================
        // TAB 4 - ALLOWED LOCATIONS
        // ============================================================

        private ILocator AllowedLocationsTab =>
            _page.Locator("a[data-toggle='tab']").Nth(3);

        private ILocator AllowedLocationsTabText =>
            _page.Locator("small.wz-desc.box-block.text-semibold").Nth(3);

        private ILocator VerifyLocationCheckbox =>
            _page.Locator("#cphBody_chkVerifyLocation");


        // ============================================================
        // TAB 5 - FIT
        // ============================================================

        private ILocator FitTab =>
            _page.Locator("a[data-toggle='tab']").Nth(4);

        private ILocator FitTabText =>
            _page.Locator("small.wz-desc.box-block.text-semibold").Nth(4);

        private ILocator TrackingDeviceDropdown =>
            _page.Locator("#cphBody_selTrackingDevice");

        private ILocator FuelSensor1Dropdown =>
            _page.Locator("#cphBody_selFuelSensor1");

        private ILocator FuelSensor2Dropdown =>
            _page.Locator("#cphBody_selFuelSensor2");

        private ILocator FuelSiphoningUnit =>
            _page.Locator("#cphBody_gallonsSpan7");

        private ILocator RecordHeader =>
            _page.Locator("#cphBody_lblRecordHeaderId");

        private ILocator FitLastReadingDate =>
            _page.Locator("#cphBody_lblFITLastReadingDate");

        private ILocator FitCurrentLocation =>
            _page.Locator("#cphBody_lblFITCurrentLocation");

        private ILocator LongitudeLatitude =>
            _page.Locator("#cphBody_lblLongitudeLatitude");

        private ILocator FitAngle =>
            _page.Locator("#cphBody_lblFITAngle");

        private ILocator FitValueBlocks =>
            _page.Locator(
                "div.col-lg-3.col-md-6.col-sm-6.col-xs-5");


        // ============================================================
        // TAB 6 - EXTERNAL SYSTEM
        // ============================================================

        private ILocator ExternalSystemTab =>
            _page.Locator("a[data-toggle='tab']").Nth(5);

        private ILocator ExternalSystemTabText =>
            _page.Locator("small.wz-desc.box-block.text-semibold").Nth(5);


        // ============================================================
        // TAB 7 - AUDIT TRAIL
        // ============================================================

        private ILocator AuditTrailTab =>
            _page.Locator("a[data-toggle='tab']").Nth(6);

        private ILocator AuditTrailTabText =>
            _page.Locator("small.wz-desc.box-block.text-semibold").Nth(6);


        // ============================================================
        // SAVE
        // ============================================================

        private ILocator SaveButton =>
            _page.Locator("div.box-inline button").Nth(3);

        private ILocator SuccessMessage =>
            _page.Locator("#cphBody_lblMessage");


        // ============================================================
        // NAVIGATION
        // ============================================================

        public async Task NavigateAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            await _page.GotoAsync(
                "https://admin-stage.payment24.co/FleetCardListV2.aspx",
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 15000
                });

            await _page.WaitForLoadStateAsync(
                LoadState.DOMContentLoaded);

            stopwatch.Stop();

            var responseTime = stopwatch.ElapsedMilliseconds;

            Assert.IsTrue(
                responseTime <= 10000,
                $"Fleet Card List V2 response time ({responseTime} ms) exceeds the 10000 ms threshold.");

            Console.WriteLine(
                $"{responseTime} ms - Fleet Card List V2 page load time.");

            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Console.WriteLine(
                "✔ Fleet Card List V2 page loaded.");
        }


        public async Task VerifyVehiclesHeadingAsync()
        {
            var heading =
                (await PageHeading.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Vehicles",
                heading,
                "Vehicles heading is incorrect.");

            Console.WriteLine(
                "✔ Vehicles heading verified.");
        }


        // ============================================================
        // SELECT FLEET AND ADD VEHICLE
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

            Console.WriteLine(
                $"✔ Fleet selected: {fleetName}");
        }


        public async Task AddVehicleAsync()
        {
            await AddVehicleButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await AddVehicleButton.ClickAsync();

            await VehicleDetailsTabText.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Console.WriteLine(
                "✔ Add Vehicle page opened.");
        }


        // ============================================================
        // TAB 1 - VEHICLE DETAILS
        // ============================================================

        public async Task VerifyVehicleDetailsTabAsync()
        {
            var text =
                (await VehicleDetailsTabText.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Vehicle Details",
                text,
                "Vehicle Details tab text is incorrect.");

            Console.WriteLine(
                "✔ Vehicle Details tab verified.");
        }


        public async Task VerifyVehicleDetailsLabelsAsync()
        {
            string[] expectedLabels =
            {
                "Customer",
                "Account Number",
                "Department",
                "Charge Card Fee",
                "Vehicle Tag Number",
                "Vehicle Status",
                "Registration Number",
                "Vehicle Type",
                "Category/Colour",
                "Vehicle Make",
                "Vehicle Colour",
                "Fuel Type",
                "Other Products",
                "Tank Size",
                "Year Model",
                "Issue Date",
                "Expiry Date",
                "Reference Number",
                "Offer Level"
            };

            await VerifyLabelsAsync(
                expectedLabels,
                "Vehicle Details");
        }


        private async Task VerifyLabelsAsync(
            string[] expectedLabels,
            string sectionName)
        {
            var labels =
                _page.Locator("div.form-group > label");

            var labelCount =
                await labels.CountAsync();

            Assert.IsTrue(
                labelCount >= expectedLabels.Length,
                $"{sectionName}: expected at least {expectedLabels.Length} labels but found {labelCount}.");

            for (int i = 0; i < expectedLabels.Length; i++)
            {
                var actual =
                    (await labels.Nth(i).InnerTextAsync()).Trim();

                Assert.AreEqual(
                    expectedLabels[i],
                    actual,
                    $"{sectionName}: incorrect label at position {i + 1}.");
            }

            Console.WriteLine(
                $"✔ {sectionName} labels verified.");
        }


        public async Task VerifyCustomerAndAccountAsync()
        {
            await CustomerValue.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var customer =
                (await CustomerValue.InnerTextAsync()).Trim();

            Assert.IsFalse(
                string.IsNullOrWhiteSpace(customer),
                "Customer name is not displayed.");

            var account =
                (await AccountNumberValue.InnerTextAsync()).Trim();

            Assert.IsFalse(
                string.IsNullOrWhiteSpace(account),
                "Account Number is not displayed.");

            Console.WriteLine(
                $"✔ Customer displayed: {customer}");

            Console.WriteLine(
                $"✔ Account Number displayed: {account}");
        }


        public async Task FillVehicleDetailsAsync()
        {
            await TankSizeField.FillAsync("50");

            Console.WriteLine(
                "✔ Tank Size entered: 50");

            await SelectDepartmentAsync();

            await ChargeCardFeeCheckbox.CheckAsync();

            await VehicleTagDropdown.ClickAsync();

            await VehicleTagResults.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await VehicleTagResults.ClickAsync();

            Console.WriteLine(
                "✔ Vehicle Tag Number selected.");

            await VehicleStatusDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = "Active"
                });

            var registration =
                DateTime.Today.ToString("ddMMyy") + "IMPL";

            await RegistrationNumberField.FillAsync(
                registration);

            await VehicleTypeDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = "Passenger Vehicle"
                });

            await VehicleCategoryDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Value = "3"
                });

            await VehicleMakeDropdown.ClickAsync();

            await VehicleMakeSearchField.Last.FillAsync("BMW");

            await VehicleMakeResults.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await VehicleMakeResults.ClickAsync();

            await VehicleColourField.FillAsync("Black");

            await YearModelDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Value = "2024"
                });

            await FuelTypeDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = "Diesel 50ppm"
                });

            await SelectIssueDateAsync();

            await SelectExpiryDateAsync();

            var reference =
                "Pay" + DateTime.Now.ToString("M");

            await ReferenceNumberField.FillAsync(reference);

            await OfferDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = "Fleet Tag"
                });

            Console.WriteLine(
                $"✔ Vehicle details populated. Registration: {registration}");
        }


        private async Task SelectDepartmentAsync()
        {
            await DepartmentDropdown.ClickAsync();

            await DepartmentSearchField.Last.FillAsync("defau");

            await DepartmentResults.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await DepartmentResults.ClickAsync();

            Console.WriteLine(
                "✔ Department selected: Default");
        }


        private async Task SelectIssueDateAsync()
        {
            await IssueDateField.ClickAsync();

            await IssueDateHeader.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 5000
                });

            await IssueDateHeader.ClickAsync();
            await IssueDateTitle.ClickAsync();

            await IssueDateYear.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 5000
                });

            await IssueDateYear.ClickAsync();
            await IssueDateMonth.ClickAsync();
            await IssueDateDay.ClickAsync();

            Console.WriteLine(
                "✔ Issue Date selected.");
        }


        private async Task SelectExpiryDateAsync()
        {
            // Open the expiry date calendar
            await ExpiryDateCalendarButton.ClickAsync();

            // Get the visible calendar
            var calendar = _page.Locator(".ajax__calendar_container:visible");

            await calendar.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000
            });

            // Open year selection
            var calendarTitle = calendar.Locator(".ajax__calendar_title").First;

            await calendarTitle.ClickAsync();

            // Select the visible year
            var year = calendar.Locator(".ajax__calendar_year:visible").First;

            await year.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000
            });

            await year.ClickAsync();

            // Select the visible month
            var month = calendar.Locator(".ajax__calendar_month:visible").First;

            await month.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000
            });

            await month.ClickAsync();

            // Select the visible day
            var day = calendar.Locator(".ajax__calendar_day:visible").First;

            await day.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 5000
            });

            await day.ClickAsync();
        }


        // ============================================================
        // TAB 2 - VEHICLE MAINTENANCE
        // ============================================================

        public async Task OpenVehicleMaintenanceTabAsync()
        {
            await VehicleMaintenanceTab.ClickAsync();

            await VehicleMaintenanceTabText.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var text =
                (await VehicleMaintenanceTabText.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Vehicle Maintenance",
                text,
                "Vehicle Maintenance tab text is incorrect.");

            Console.WriteLine(
                "✔ Vehicle Maintenance tab verified.");
        }


        public async Task VerifyVehicleMaintenanceAsync()
        {
            string[] expectedLabels =
            {
                "Capture ODO",
                "Current Odo Reading (km)",
                "Expected Fuel Consumption",
                "Actual Fuel Consumption",
                "Service Interval (km)",
                "Last Service Odo (km)",
                "Notification ODO Reading",
                "Last Service Date",
                "Next Service Odo (km)",
                "Service Cycle",
                "Next Service Date",
                "Next Oil Change Date",
                "Next Tyre Change Date",
                "Next Battery Change Date",
                "Next Oil Change Odo (km)",
                "Tyre Tread Width",
                "Tyre Aspect Ratio",
                "Wheel Diameter",
                "License Expiry Date",
                "VIN Number",
                "Engine Number",
                "Enable Telematics Portal",
                "Insurance Policy Number",
                "Insurance Company Name",
                "Next Insurance Review"
            };

            await VerifyLabelsAsync(
                expectedLabels,
                "Vehicle Maintenance");

            Assert.IsFalse(
                await CaptureOdoCheckbox.IsCheckedAsync(),
                "Capture ODO is checked by default.");

            Assert.IsFalse(
                await EnableTelematicsCheckbox.IsCheckedAsync(),
                "Enable Telematics Portal is checked by default.");

            await VerifyUnitTextAsync(
                CurrentOdoField,
                "km");

            await VerifyUnitTextAsync(
                ExpectedFuelConsumptionField,
                "gallons per 100km");

            await VerifyUnitTextAsync(
                ActualFuelConsumptionField,
                "gallons per 100km");

            await VerifyUnitTextAsync(
                ServiceIntervalField,
                "km");

            await VerifyUnitTextAsync(
                LastServiceOdoField,
                "km");

            await VerifyUnitTextAsync(
                NotificationOdoField,
                "km Prior Service");

            await VerifyUnitTextAsync(
                NextServiceOdoField,
                "km");

            await VerifyUnitTextAsync(
                NextOilChangeOdoField,
                "km");

            await ServiceCycleDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Value = "Monthly"
                });

            var serviceCycleOptions =
                ServiceCycleDropdown.Locator("option");

            var count =
                await serviceCycleOptions.CountAsync();

            Assert.AreEqual(
                2,
                count,
                $"Expected 2 Service Cycle options but found {count}.");

            await NextOilChangeOdoField.FillAsync("");

            Console.WriteLine(
                "✔ Vehicle Maintenance fields verified.");
        }


        private async Task VerifyUnitTextAsync(
            ILocator field,
            string expectedText)
        {
            var parent =
                field.Locator("xpath=..");

            var parentText =
                (await parent.InnerTextAsync()).Trim();

            Assert.IsTrue(
                parentText.Contains(expectedText),
                $"Expected unit '{expectedText}' was not found for field.");

            Console.WriteLine(
                $"✔ Unit verified: {expectedText}");
        }


        // ============================================================
        // TAB 3 - FILLING RULES
        // ============================================================

        public async Task OpenFillingRulesTabAsync()
        {
            await FillingRulesTab.ClickAsync();

            await FillingRulesTabText.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var text =
                (await FillingRulesTabText.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Filling Rules and Limits",
                text,
                "Filling Rules and Limits tab text is incorrect.");

            Console.WriteLine(
                "✔ Filling Rules and Limits tab verified.");
        }


        public async Task VerifyFillingRulesAsync()
        {
            string[] expectedLabels =
            {
                "Authorize Override Allowed",
                "Temporary Disable Filling Rules",
                "Available Balance",
                "Start Time Hour",
                "End Time Hour",
                "Select days of the week",
                "Verify Product",
                "Fueling Criteria",
                "Amount Per Transaction",
                "Amount Per Day",
                "Amount Per Week",
                "Amount Per Month",
                "Volume Per Transaction",
                "Volume Per Day",
                "Volume Per Week",
                "Volume Per Month",
                "Transaction Per Day",
                "Transaction Per Week",
                "Transaction Per Month",
                "Oil Limit",
                "Number Of Incorrect Pin Entry",
                "No. of Attempts for Lock out",
                "Card Lock out Period Minute",
                "Velocity Minutes",
                "Minimum Kilometers Between Transaction",
                "Maximum Kilometers Between Transaction"
            };

            await VerifyLabelsAsync(
                expectedLabels,
                "Filling Rules and Limits");

            Assert.IsTrue(
                await AuthorizeOverrideCheckbox.IsCheckedAsync(),
                "Authorize Override checkbox is not enabled by default.");

            var reasonText =
                await _page
                    .Locator("span[style='color:Red;']")
                    .InnerTextAsync();

            Assert.AreEqual(
                "Explain reason for authorisation override",
                reasonText.Trim());

            await ReasonForOverrideField.FillAsync(
                "AddVehicleTest" + DateTime.Now.ToString("d"));

            var balance =
                (await AccountBalance.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "0",
                balance,
                "Available Balance is not 0.");

            if (!int.TryParse(balance, out var balanceValue))
            {
                Assert.Fail(
                    "Available Balance could not be parsed as a number.");
            }

            Assert.IsTrue(
                balanceValue <= 0,
                $"Available Balance is greater than 0: {balanceValue}");

            Console.WriteLine(
                "✔ Available Balance verified as 0.");

            await VerifyUnitByIndexAsync(32, "gallons");
            await VerifyUnitByIndexAsync(33, "gallons");
            await VerifyUnitByIndexAsync(34, "gallons");
            await VerifyUnitByIndexAsync(35, "gallons");

            await VerifyUnitByIndexAsync(36, "Minutes");
            await VerifyUnitByIndexAsync(37, "Minutes");

            Console.WriteLine(
                "✔ Filling Rules and Limits verified.");
        }


        private async Task VerifyUnitByIndexAsync(
            int index,
            string expectedText)
        {
            var element =
                _page.Locator("div span").Nth(index - 1);

            await element.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 5000
                });

            var text =
                (await element.InnerTextAsync()).Trim();

            Assert.AreEqual(
                expectedText,
                text,
                $"Expected '{expectedText}' at span position {index} but found '{text}'.");

            Console.WriteLine(
                $"✔ '{expectedText}' string verified.");
        }


        // ============================================================
        // TAB 4 - ALLOWED LOCATIONS
        // ============================================================

        public async Task OpenAllowedLocationsTabAsync()
        {
            await AllowedLocationsTab.ClickAsync();

            await AllowedLocationsTabText.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var text =
                (await AllowedLocationsTabText.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Allowed Locations",
                text,
                "Allowed Locations tab text is incorrect.");

            Console.WriteLine(
                "✔ Allowed Locations tab verified.");
        }


        public async Task VerifyAllowedLocationsAsync()
        {
            string[] expectedLabels =
            {
                "Verify Location",
                "Merchant",
                "Merchant Cluster",
                "Merchant Category Rules"
            };

            await VerifyLabelsAsync(
                expectedLabels,
                "Allowed Locations");

            Assert.IsFalse(
                await VerifyLocationCheckbox.IsCheckedAsync(),
                "Verify Location checkbox is checked by default.");

            Console.WriteLine(
                "✔ Verify Location checkbox is not checked.");
        }


        // ============================================================
        // TAB 5 - FIT
        // ============================================================

        public async Task OpenFitTabAsync()
        {
            await FitTab.ClickAsync();

            await FitTabText.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var text =
                (await FitTabText.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "FIT - Fuel in Tank and Tracking",
                text,
                "FIT tab text is incorrect.");

            Console.WriteLine(
                "✔ FIT - Fuel in Tank and Tracking tab verified.");
        }


        public async Task VerifyFitAsync()
        {
            string[] expectedLabels =
            {
                "Tracking Device",
                "Tank 1 - Fuel Sensor",
                "Tank 2 - Fuel Sensor",
                "Fuel Siphoning Threshold",
                "Last Reading",
                "Record Header",
                "Date",
                "Current location",
                "Longitude / Latitude",
                "Current Fuel Level in Tank 1",
                "Current Fuel Level in Tank 2",
                "Speed",
                "Altitude",
                "Angle"
            };

            await VerifyLabelsAsync(
                expectedLabels,
                "FIT");

            await TrackingDeviceDropdown.SelectOptionAsync(
                new SelectOptionValue { Value = "0" });

            await FuelSensor1Dropdown.SelectOptionAsync(
                new SelectOptionValue { Value = "0" });

            await FuelSensor2Dropdown.SelectOptionAsync(
                new SelectOptionValue { Value = "0" });

            var gallons =
                (await FuelSiphoningUnit.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "gallons",
                gallons,
                "Gallons unit is not displayed.");

            Assert.AreEqual(
                "N/A",
                (await RecordHeader.InnerTextAsync()).Trim());

            Assert.AreEqual(
                "N/A",
                (await FitLastReadingDate.InnerTextAsync()).Trim());

            Assert.AreEqual(
                "N/A",
                (await FitCurrentLocation.InnerTextAsync()).Trim());

            Assert.AreEqual(
                "N/A",
                (await LongitudeLatitude.InnerTextAsync()).Trim());

            Assert.AreEqual(
                "N/A",
                (await FitAngle.InnerTextAsync()).Trim());

            var blocks =
                FitValueBlocks;

            Assert.AreEqual(
                "N/A Gallons",
                (await blocks.Nth(9).InnerTextAsync()).Trim());

            Assert.AreEqual(
                "N/A Gallons",
                (await blocks.Nth(10).InnerTextAsync()).Trim());

            Assert.AreEqual(
                "N/A km/h",
                (await blocks.Nth(11).InnerTextAsync()).Trim());

            Assert.AreEqual(
                "N/A meters",
                (await blocks.Nth(12).InnerTextAsync()).Trim());

            Console.WriteLine(
                "✔ FIT fields and default values verified.");
        }


        // ============================================================
        // TAB 6 - EXTERNAL SYSTEM
        // ============================================================

        public async Task OpenExternalSystemTabAsync()
        {
            await ExternalSystemTab.ClickAsync();

            await ExternalSystemTabText.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var text =
                (await ExternalSystemTabText.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "External System",
                text,
                "External System tab text is incorrect.");

            Console.WriteLine(
                "✔ External System tab verified.");
        }


        public async Task VerifyExternalSystemAsync()
        {
            var labels =
                _page.Locator("div.form-group > label");

            var labelTexts =
                await labels.AllInnerTextsAsync();

            Assert.IsTrue(
                labelTexts.Any(
                    x => x.Trim() == "External System"),
                "External System label was not found.");

            Console.WriteLine(
                "✔ External System field verified.");
        }


        // ============================================================
        // TAB 7 - AUDIT TRAIL
        // ============================================================

        public async Task OpenAuditTrailTabAsync()
        {
            await AuditTrailTab.ClickAsync();

            await AuditTrailTabText.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var text =
                (await AuditTrailTabText.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Audit Trail",
                text,
                "Audit Trail tab text is incorrect.");

            Console.WriteLine(
                "✔ Audit Trail tab verified.");
        }


        public async Task VerifyAuditTrailAsync()
        {
            string[] expectedLabels =
            {
                "Created By",
                "Created Date",
                "Last Modified By",
                "Last Modified Date"
            };

            var labels =
                _page.Locator("div.form-group > label");

            var labelTexts =
                await labels.AllInnerTextsAsync();

            foreach (var expected in expectedLabels)
            {
                Assert.IsTrue(
                    labelTexts.Any(
                        x => x.Trim() == expected),
                    $"Audit Trail label '{expected}' was not found.");
            }

            Console.WriteLine(
                "✔ Audit Trail fields verified.");
        }


        // ============================================================
        // SAVE
        // ============================================================

        public async Task SaveAsync()
        {
            await SaveButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await SaveButton.IsEnabledAsync(),
                "Save button is disabled.");

            Console.WriteLine(
                "✔ Save button is visible and enabled.");

            await SaveButton.ClickAsync();

            Console.WriteLine(
                "✔ Save button clicked.");
        }


        public async Task VerifySaveResultAsync()
        {
            try
            {
                await SuccessMessage.WaitForAsync(
                    new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 10000
                    });

                var message =
                    (await SuccessMessage.InnerTextAsync()).Trim();

                Assert.AreEqual(
                    "Vehicle updated successfully.",
                    message,
                    "Vehicle update success message is incorrect.");

                Console.WriteLine(
                    $"✔ {message}");
            }
            catch (TimeoutException)
            {
                Console.WriteLine(
                    "❌ Vehicle update success message was not displayed.");

                Console.WriteLine(
                    $"Current URL: {_page.Url}");

                Assert.Fail(
                    "PORTAL DEFECT: Vehicle update did not complete successfully. " +
                    "The Fleet Card Maintain V2 page did not display " +
                    "'Vehicle updated successfully.' after clicking Save.");
            }
        }
    }
}