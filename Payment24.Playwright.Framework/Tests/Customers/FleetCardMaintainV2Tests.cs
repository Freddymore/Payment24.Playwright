using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class FleetCardMaintainV2Tests : BaseTest
    {
        [TestMethod]
        public async Task Fleet_Card_Maintain_V2()
        {
            // ========================================================
            // LOGIN
            // ========================================================

            await StartPortalSessionAsync("IMPL");

            var page =
                new FleetCardMaintainV2Page(Page);


            // ========================================================
            // FLEET CARD LIST V2
            // ========================================================

            await page.NavigateAsync();

            await page.VerifyVehiclesHeadingAsync();

            await page.SelectFleetAsync(
                "DO NOT DELETE");

            await page.AddVehicleAsync();


            // ========================================================
            // TAB 1 - VEHICLE DETAILS
            // ========================================================

            await page.VerifyVehicleDetailsTabAsync();

            await page.VerifyVehicleDetailsLabelsAsync();

            await page.VerifyCustomerAndAccountAsync();

            await page.FillVehicleDetailsAsync();


            // ========================================================
            // TAB 2 - VEHICLE MAINTENANCE
            // ========================================================

            await page.OpenVehicleMaintenanceTabAsync();

            await page.VerifyVehicleMaintenanceAsync();


            // ========================================================
            // TAB 3 - FILLING RULES AND LIMITS
            // ========================================================

            await page.OpenFillingRulesTabAsync();

            await page.VerifyFillingRulesAsync();


            // ========================================================
            // TAB 4 - ALLOWED LOCATIONS
            // ========================================================

            await page.OpenAllowedLocationsTabAsync();

            await page.VerifyAllowedLocationsAsync();


            // ========================================================
            // TAB 5 - FIT
            // ========================================================

            await page.OpenFitTabAsync();

            await page.VerifyFitAsync();


            // ========================================================
            // TAB 6 - EXTERNAL SYSTEM
            // ========================================================

            await page.OpenExternalSystemTabAsync();

            await page.VerifyExternalSystemAsync();


            // ========================================================
            // TAB 7 - AUDIT TRAIL
            // ========================================================

            await page.OpenAuditTrailTabAsync();

            await page.VerifyAuditTrailAsync();


            // ========================================================
            // SAVE
            // ========================================================

            await page.SaveAsync();

            await page.VerifySaveResultAsync();
        }
    }
}