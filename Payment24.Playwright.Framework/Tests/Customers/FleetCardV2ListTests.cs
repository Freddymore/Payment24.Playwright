
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers
{
    [TestClass]
    public class FleetCardListV2Tests : BaseTest
    {
        [TestMethod]
        public async Task Fleet_Card_List_V2()
        {
            // ========================================================
            // LOGIN
            // ========================================================

            await StartPortalSessionAsync("IMPL");

            // ========================================================
            // NAVIGATE TO FLEET CARD LIST V2
            // ========================================================

            var fleetCardListV2Page =
                new FleetCardListV2Page(Page);

            var startTime = DateTime.UtcNow;

            await fleetCardListV2Page.NavigateAsync();

            var pageLoadTime =
                (DateTime.UtcNow - startTime).TotalMilliseconds;

            Assert.IsTrue(
                pageLoadTime <= 10000,
                $"Fleet Card List V2 response time ({pageLoadTime:F0} ms) exceeds the threshold of 10000 ms.");

            Console.WriteLine(
                $"{pageLoadTime:F0} ms - Time taken to load Fleet Card List V2.");

            await fleetCardListV2Page.VerifyPageLoadedAsync();

            // ========================================================
            // VERIFY PAGE HEADER
            // ========================================================

            await fleetCardListV2Page.VerifyHeaderAsync();

            // ========================================================
            // SELECT FLEET
            // ========================================================

            await fleetCardListV2Page.SelectFleetAsync("DO NOT DELETE");

            // ========================================================
            // VERIFY STATUS DROPDOWN
            // ========================================================

            await fleetCardListV2Page.VerifyStatusDropdownAsync();

            // ========================================================
            // SEARCH
            // ========================================================

            await fleetCardListV2Page.SearchAsync();

            // ========================================================
            // VERIFY SHOW ENTRIES
            // ========================================================

            await fleetCardListV2Page.VerifyShowEntriesAsync();

            // ========================================================
            // FILTER BY CARD NUMBER
            // ========================================================

            await fleetCardListV2Page.FilterByCardNumberAsync(
                "5000010000020147");

            // ========================================================
            // VERIFY TABLE HEADERS
            // ========================================================

            await fleetCardListV2Page.VerifyTableHeadersAsync();

            // ========================================================
            // VERIFY COLUMN DATA
            // ========================================================

            await fleetCardListV2Page.VerifyColumnDataAsync();

            // ========================================================
            // VERIFY ACTION LINKS
            // ========================================================

            await fleetCardListV2Page.VerifyActionLinksAsync();

            Console.WriteLine(
                "✔ Fleet Card List V2 regression test completed successfully.");
        }
    }
}