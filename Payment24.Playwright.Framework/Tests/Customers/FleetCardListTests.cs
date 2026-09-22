using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class FleetCardListTests : BaseTest
    {
        [TestMethod]
        public async Task Fleet_Card_List()
        {
            // ========================================================
            // LOGIN
            // ========================================================

            await StartPortalSessionAsync("IMPL");

            // ========================================================
            // FLEET CARD LIST PAGE
            // ========================================================

            var fleetCardListPage = new FleetCardListPage(Page);

            var startTime = DateTime.UtcNow;

            await fleetCardListPage.NavigateAsync();

            var pageLoadTime =
                (DateTime.UtcNow - startTime).TotalMilliseconds;

            Assert.IsTrue(
                pageLoadTime <= 15000,
                $"Fleet Card List page response time ({pageLoadTime:F0} ms) exceeds the threshold of 15000 ms.");

            Console.WriteLine(
                $"{pageLoadTime:F0} ms - The time it took to load the Fleet Card List page.");

            await fleetCardListPage.VerifyPageLoadedAsync();

            // ========================================================
            // VERIFY PAGE HEADER
            // ========================================================

            await fleetCardListPage.VerifyHeaderAsync();

            // ========================================================
            // SELECT FLEET
            // ========================================================

            await fleetCardListPage.SelectFleetAsync(
                "DO NOT DELETE");

            // ========================================================
            // VERIFY STATUS DROPDOWN
            // ========================================================

            await fleetCardListPage.VerifyStatusDropdownAsync();

            // ========================================================
            // SEARCH
            // ========================================================

            await fleetCardListPage.SearchAsync();

            // ========================================================
            // VERIFY SHOW ENTRIES
            // ========================================================

            await fleetCardListPage.VerifyShowEntriesAsync();

            // ========================================================
            // FILTER BY CARD NUMBER
            // ========================================================

            await fleetCardListPage.FilterByCardNumberAsync(
                "5000010000020147");

            // ========================================================
            // VERIFY TABLE HEADERS
            // ========================================================

            await fleetCardListPage.VerifyTableHeadersAsync();

            // ========================================================
            // VERIFY COLUMN DATA
            // ========================================================

            await fleetCardListPage.VerifyColumnDataAsync();

            // ========================================================
            // VERIFY ACTION LINKS
            // ========================================================

            await fleetCardListPage.VerifyActionLinksAsync();

            Console.WriteLine(
                "✔ Fleet Card List regression test completed successfully.");
        }
    }
}