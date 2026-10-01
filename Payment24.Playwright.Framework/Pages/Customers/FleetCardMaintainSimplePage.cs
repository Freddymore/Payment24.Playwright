using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class FleetCardMaintainSimplePage
    {
        private readonly IPage _page;

        public FleetCardMaintainSimplePage(IPage page)
        {
            _page = page;
        }

        // Page elements
        private ILocator PageHeading =>
            _page.Locator("h1");

        private ILocator Breadcrumbs =>
            _page.Locator("a.breadcrumb");

        private ILocator SaveButton =>
            _page.Locator("#btn-save-fake");

        private ILocator CancelButton =>
            _page.Locator("#cphBody_btnCancel");

        private ILocator CustomerDropdown =>
            _page.Locator("#cphBody_selFleet");

        private ILocator AccountNumberField =>
            _page.Locator("#cphBody_lblCustomerMobileNumber");

        private ILocator DepartmentDropdown =>
            _page.Locator("#cphBody_selFleetDepartment");

        private ILocator MobileNumberField =>
            _page.Locator("#cphBody_txtMobileNumber");

        private ILocator MobileNumberPrompt =>
            _page.Locator("div.col-lg-2").First;

        private ILocator VehicleTagNumberDropdown =>
            _page.Locator("#cphBody_selVehicleCard");

        private ILocator RegistrationNumberField =>
            _page.Locator("#cphBody_txtRegistrationNumber");

        private ILocator MakeDropdown =>
            _page.Locator("#cphBody_selMake");

        private ILocator VehicleModelField =>
            _page.Locator("#cphBody_txtModel");

        private ILocator ColourField =>
            _page.Locator("#cphBody_txtColour");

        private ILocator FuelTypeDropdown =>
            _page.Locator("#cphBody_selMerchantProduct");

        private ILocator TankSizeField =>
            _page.Locator("#cphBody_txtTankSize");

        private ILocator YearModelDropdown =>
            _page.Locator("#cphBody_selYearModel");

        private ILocator SuccessMessage =>
            _page.Locator("div.alert.alert-success");

        // Required field validation messages
        private ILocator MobileNumberValidation =>
            _page.Locator("#cphBody_valMobileNumber");

        private ILocator VehicleCardValidation =>
            _page.Locator("#cphBody_valVehicleCard");

        private ILocator RegistrationNumberValidation =>
            _page.Locator("#cphBody_valRegistrationNumber");

        private ILocator ModelValidation =>
            _page.Locator("#cphBody_valModel");

        private ILocator TankSizeValidation =>
            _page.Locator("#cphBody_valTankSize");


        public async Task NavigateAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            await _page.GotoAsync(
                "https://admin-stage.payment24.co/FleetCardMaintainSimple.aspx",
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 20000
                });

            await _page.WaitForLoadStateAsync(
                LoadState.DOMContentLoaded);

            stopwatch.Stop();

            var responseTime = stopwatch.ElapsedMilliseconds;

            Assert.IsTrue(
                responseTime <= 10000,
                $"Fleet Card Maintain Simple page response time ({responseTime} ms) exceeds the threshold of 10000 ms.");

            Console.WriteLine(
                $"{responseTime} ms - Fleet Card Maintain Simple page load time.");

            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Console.WriteLine(
                "✔ Fleet Card Maintain Simple page loaded.");
        }


        public async Task VerifyPageHeaderAsync()
        {
            var heading = (await PageHeading.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Add Vehicle",
                heading,
                "Fleet Card Maintain Simple page heading is incorrect.");

            Console.WriteLine(
                "✔ Page heading 'Add Vehicle' verified.");
        }


        public async Task VerifyBreadcrumbAsync()
        {
            var breadcrumbCount = await Breadcrumbs.CountAsync();

            Assert.IsTrue(
                breadcrumbCount >= 2,
                $"Expected at least 2 breadcrumbs but found {breadcrumbCount}.");

            var secondBreadcrumb =
                (await Breadcrumbs.Nth(1).InnerTextAsync()).Trim();

            Assert.AreEqual(
                "Simple Fleet Card Details",
                secondBreadcrumb,
                "Second breadcrumb is incorrect.");

            Console.WriteLine(
                "✔ 'Simple Fleet Card Details' breadcrumb verified.");
        }


        public async Task VerifyRequiredFieldValidationAsync()
        {
            await SaveButton.ClickAsync();

            var validationMessages = new Dictionary<string, ILocator>
            {
                { "Mobile Number", MobileNumberValidation },
                { "Vehicle Card", VehicleCardValidation },
                { "Registration Number", RegistrationNumberValidation },
                { "Model", ModelValidation },
                { "Tank Size", TankSizeValidation }
            };

            foreach (var validation in validationMessages)
            {
                await validation.Value.WaitForAsync(
                    new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 5000
                    });

                Assert.IsTrue(
                    await validation.Value.IsVisibleAsync(),
                    $"{validation.Key} required validation message is not displayed.");

                Console.WriteLine(
                    $"✔ {validation.Key} required validation displayed.");
            }
        }


        public async Task VerifyFormLabelsAsync()
        {
            string[] expectedLabels =
            {
                "Customer",
                "Account Number",
                "Department",
                "Mobile Number",
                "Vehicle Tag Number",
                "Registration Number",
                "Vehicle Make",
                "Vehicle Model",
                "Colour",
                "Fuel Type",
                "Tank Size",
                "Year Model"
            };

            var labels =
                _page.Locator("label.col-lg-3.control-label");

            var labelCount = await labels.CountAsync();

            Assert.AreEqual(
                expectedLabels.Length,
                labelCount,
                $"Expected {expectedLabels.Length} form labels but found {labelCount}.");

            for (int i = 0; i < expectedLabels.Length; i++)
            {
                var actualLabel =
                    (await labels.Nth(i).InnerTextAsync()).Trim();

                Assert.AreEqual(
                    expectedLabels[i],
                    actualLabel,
                    $"Incorrect form label at position {i + 1}.");

                Console.WriteLine(
                    $"✔ {actualLabel} label verified.");
            }

            Console.WriteLine(
                "✔ All Fleet Card Maintain Simple form labels verified.");
        }


        public async Task VerifyCancelButtonAsync()
        {
            await CancelButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.IsTrue(
                await CancelButton.IsVisibleAsync(),
                "Cancel button is not displayed.");

            Assert.IsTrue(
                await CancelButton.IsEnabledAsync(),
                "Cancel button is not enabled.");

            Console.WriteLine(
                "✔ Cancel button is displayed and enabled.");
        }


        public async Task SelectCustomerAsync()
        {
            await CustomerDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = "IMPL3086010 - DO NOT DELETE"
                });

            Console.WriteLine(
                "✔ Customer selected: IMPL3086010 - DO NOT DELETE");
        }


        public async Task VerifyAccountNumberAsync()
        {
            await AccountNumberField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 12000
                });

            var accountNumber =
                (await AccountNumberField.InnerTextAsync()).Trim();

            Assert.AreEqual(
                "IMPL3086010",
                accountNumber,
                "Account number is incorrect after selecting the customer.");

            Console.WriteLine(
                $"✔ Account Number verified: {accountNumber}");
        }


        public async Task SelectDepartmentAsync()
        {
            await DepartmentDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = "Default"
                });

            Console.WriteLine(
                "✔ Department selected: Default");
        }


        public async Task EnterMobileNumberAsync()
        {
            await MobileNumberField.FillAsync("27729053339");

            await MobileNumberPrompt.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 5000
                });

            Assert.IsTrue(
                await MobileNumberPrompt.IsVisibleAsync(),
                "Mobile number prompt is not displayed.");

            var promptText =
                (await MobileNumberPrompt.InnerTextAsync()).Trim();

            Console.WriteLine(
                $"✔ Mobile number prompt displayed: {promptText}");
        }


        public async Task SelectVehicleTagAsync()
        {
            var options =
                VehicleTagNumberDropdown.Locator("option");

            var optionCount =
                await options.CountAsync();

            Assert.IsTrue(
                optionCount > 1,
                "Vehicle Tag Number dropdown does not contain enough options.");

            await VehicleTagNumberDropdown.SelectOptionAsync(
                 new SelectOptionValue
                 {
                     Index = 1
                 });

            Console.WriteLine(
                "✔ Vehicle Tag Number selected.");
        }


        public async Task EnterRegistrationNumberAsync()
        {
            var registrationNumber =
                "REG" + DateTime.Today.ToString("ddMMyy");

            await RegistrationNumberField.FillAsync(
                registrationNumber);

            Console.WriteLine(
                $"✔ Registration Number entered: {registrationNumber}");
        }


        public async Task SelectVehicleMakeAsync()
        {
            await MakeDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = "AUDI"
                });

            Console.WriteLine(
                "✔ Vehicle Make selected: AUDI");
        }


        public async Task EnterVehicleModelAsync()
        {
            await VehicleModelField.FillAsync("Sedan");

            Console.WriteLine(
                "✔ Vehicle Model entered: Sedan");
        }


        public async Task EnterColourAsync()
        {
            await ColourField.FillAsync("Black");

            Console.WriteLine(
                "✔ Colour entered: Black");
        }


        public async Task SelectFuelTypeAsync()
        {
            await FuelTypeDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Value = "Test Code"
                });

            Console.WriteLine(
                "✔ Fuel Type selected: Diesel - 50 ppm Test");
        }


        public async Task EnterTankSizeAsync()
        {
            await TankSizeField.FillAsync("20");

            Console.WriteLine(
                "✔ Tank Size entered: 20");
        }


        public async Task SelectYearModelAsync()
        {
            await YearModelDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Value = "2025"
                });

            Console.WriteLine(
                "✔ Year Model selected: 2025");
        }


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

            Console.WriteLine("✔ Save button is visible and enabled.");

            try
            {
                await SaveButton.ClickAsync(
                    new LocatorClickOptions
                    {
                        Timeout = 10000
                    });

                Console.WriteLine("✔ Save button click completed.");
            }
            catch (TimeoutException)
            {
                Console.WriteLine(
                    "❌ Save operation timed out after the button was clicked.");

                Console.WriteLine(
                    $"Current URL: {_page.Url}");

                Assert.Fail(
                    "The Fleet Card Save operation timed out. " +
                    "The application did not complete the save/postback.");
            }
        }


        public async Task VerifySaveResultAsync()
        {
            Console.WriteLine("Waiting for Fleet Card save operation...");

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

                Console.WriteLine(
                    $"✔ Fleet Card details saved successfully: {message}");
            }
            catch (TimeoutException)
            {
                Console.WriteLine(
                    "❌ Fleet Card save operation did not complete.");

                Console.WriteLine(
                    "❌ The Save button caused the portal to continue rendering/buffering " +
                    "without displaying a successful save response.");

                Assert.Fail(
                    "PORTAL DEFECT: Fleet Card Maintain Simple page does not save the vehicle. " +
                    "After clicking Save, the page continues rendering/buffering and no " +
                    "success message is displayed within 10 seconds.");
            }
        }
    }
    }
