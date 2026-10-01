using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class FleetCardMaintainSimpleTests : BaseTest
    {
        [TestMethod]
        public async Task Fleet_Card_Maintain_Simple()
        {
            // Login using the existing framework session.
            await StartPortalSessionAsync("IMPL");

            var fleetCardMaintainSimplePage =
                new FleetCardMaintainSimplePage(Page);

            // Navigate to Fleet Card Maintain Simple page.
            await fleetCardMaintainSimplePage.NavigateAsync();

            // Verify page header.
            await fleetCardMaintainSimplePage.VerifyPageHeaderAsync();

            // Verify breadcrumb.
            await fleetCardMaintainSimplePage.VerifyBreadcrumbAsync();

            // Click Save without completing the form
            // and verify required field validation.
            await fleetCardMaintainSimplePage
                .VerifyRequiredFieldValidationAsync();

            // Verify all form labels.
            await fleetCardMaintainSimplePage
                .VerifyFormLabelsAsync();

            // Verify Cancel button.
            await fleetCardMaintainSimplePage
                .VerifyCancelButtonAsync();

            // Select customer.
            await fleetCardMaintainSimplePage
                .SelectCustomerAsync();

            // Verify account number populated.
            await fleetCardMaintainSimplePage
                .VerifyAccountNumberAsync();

            // Select department.
            await fleetCardMaintainSimplePage
                .SelectDepartmentAsync();

            // Enter mobile number.
            await fleetCardMaintainSimplePage
                .EnterMobileNumberAsync();

            // Select vehicle tag.
            await fleetCardMaintainSimplePage
                .SelectVehicleTagAsync();

            // Enter registration number.
            await fleetCardMaintainSimplePage
                .EnterRegistrationNumberAsync();

            // Select vehicle make.
            await fleetCardMaintainSimplePage
                .SelectVehicleMakeAsync();

            // Enter vehicle model.
            await fleetCardMaintainSimplePage
                .EnterVehicleModelAsync();

            // Enter colour.
            await fleetCardMaintainSimplePage
                .EnterColourAsync();

            // Select fuel type.
            await fleetCardMaintainSimplePage
                .SelectFuelTypeAsync();

            // Enter tank size.
            await fleetCardMaintainSimplePage
                .EnterTankSizeAsync();

            // Select year model.
            await fleetCardMaintainSimplePage
                .SelectYearModelAsync();

            // Save Fleet Card details.
            await fleetCardMaintainSimplePage.SaveAsync();

            // Verify successful save.
            await fleetCardMaintainSimplePage
                    .VerifySaveResultAsync();
        }
    }
}