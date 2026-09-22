using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class FleetCardAllowedDriverListTests : BaseTest
    {
        [TestMethod]
        public async Task Fleet_Card_Allowed_Driver_List()
        {
            // ============================================================
            // LOGIN
            // ============================================================

            await StartPortalSessionAsync("IMPL");

            // ============================================================
            // CUSTOMER MANAGEMENT / FLEET LIST
            // ============================================================

            var customerListPage =
                new CustomerListPage    (Page);

            await customerListPage.NavigateAsync();

            await customerListPage.VerifyPageLoadedAsync();

            await customerListPage.VerifyHeaderAsync();

            // ============================================================
            // SEARCH FLEET
            // ============================================================

           await customerListPage.SearchFleetAsync(
                "DO NOT DELETE");

            // ============================================================
            // OPEN LINK VEHICLES TO DRIVER
            // ============================================================

            await customerListPage.ClickLinkVehiclesToDriverAsync();

            // ============================================================
            // LINK VEHICLES TO DRIVER PAGE
            // ============================================================

            var allowedDriverListPage =
                new FleetCardAllowedDriverListPage(Page);

            await allowedDriverListPage.VerifyPageAsync();

            await allowedDriverListPage.VerifyFleetInformationAsync();

            // ============================================================
            // ALLOW ALL VEHICLES WITHOUT SELECTING DRIVER
            // ============================================================

            await allowedDriverListPage.VerifyAllowAllWithoutDriverAsync();

            // ============================================================
            // SELECT DRIVER
            // ============================================================

            await allowedDriverListPage.SelectDriverAsync(
                "Tester QA");

           await allowedDriverListPage.VerifyDriverControlsAsync();

            // ============================================================
            // UNLINK VEHICLES
            // ============================================================

            await allowedDriverListPage.UnlinkVehiclesAsync();

            await allowedDriverListPage.VerifyNoVehiclesLinkedAsync();

            // ============================================================
            // ALLOW SELECTED WITHOUT SELECTING VEHICLE
            // ============================================================

            await allowedDriverListPage.VerifyAllowSelectedWithoutSelectionAsync();

            // ============================================================
            // UNCHECK FIRST VEHICLE
            // ============================================================

            await allowedDriverListPage.SelectFirstVehicleAsync();

            // ============================================================
            // ALLOW SELECTED VEHICLES
            // ============================================================

            await allowedDriverListPage.AllowSelectedVehiclesAsync();

            await allowedDriverListPage.VerifyVehiclesLinkedAsync();

            // ============================================================
            // UNLINK VEHICLES AGAIN
            // ============================================================

            await allowedDriverListPage.UnlinkVehiclesAsync();

            await allowedDriverListPage.VerifyNoVehiclesLinkedAsync();

            // ============================================================
            // TABLE HEADERS
            // ============================================================

            await allowedDriverListPage.VerifyTableHeadersAsync();

            // ============================================================
            // TABLE DATA
            // ============================================================

            await allowedDriverListPage.VerifyTableDataAsync();

            // ============================================================
            // TEST COMPLETE
            // ============================================================

            Console.WriteLine();
            Console.WriteLine(
                "================================================");

            Console.WriteLine(
                "FLEET CARD ALLOWED DRIVER LIST TEST PASSED");

            Console.WriteLine(
                "================================================");
        }
    }
}