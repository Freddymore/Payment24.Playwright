using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class DriverNotificationListPage
    {
        private readonly IPage _page;

        public DriverNotificationListPage(IPage page)
        {
            _page = page;
        }

        // ============================================================
        // LOCATORS
        // ============================================================

        private ILocator CustomerManagementIcon =>
            _page.Locator(
                "img[src='images/icons/customer_management_icon@2x.png']");

        private ILocator PageHeading =>
            _page.Locator("h2.heading");

        private ILocator CampaignsBreadcrumb =>
            _page.Locator("a.breadcrumb.breadbtn2");

        private ILocator NotificationsBreadcrumb =>
            _page.Locator("a.breadcontainer.breadbtn3");

        private ILocator NotificationTypeDropdown =>
            _page.Locator("#cphBody_selNotificationType");

        private ILocator SearchButton =>
            _page.Locator("#cphBody_btnSearch");

        private ILocator AddNotificationButton =>
            _page.Locator("#cphBody_btnNewNotification");

        private ILocator ShowEntriesDropdown =>
            _page.Locator(
                "select[name='cphBody_grvNotification_length']");

        private ILocator NotificationTypeColumn =>
            _page.Locator("tr td:nth-child(1)").First;

        private ILocator TitleColumn =>
            _page.Locator("tr td:nth-child(2)").First;

        private ILocator StartDateColumn =>
            _page.Locator("tr td:nth-child(3)").First;

        private ILocator EndDateColumn =>
            _page.Locator("tr td:nth-child(4)").First;

        private ILocator EditNotificationButton =>
            _page.Locator(
                "a[data-original-title='Edit Notification']").First;


        // ============================================================
        // NAVIGATE TO DRIVER NOTIFICATION LIST
        // ============================================================

        public async Task NavigateAsync()
        {
            await _page.GotoAsync(
                "https://admin-stage.payment24.co/DriverNotificationList.aspx",
                new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 60000
                });

            // The original Selenium test had an additional 3 second wait.
            // We replace it with a small Playwright wait because the page
            // itself can take some time to render after DOMContentLoaded.
            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Console.WriteLine(
                "✔ Driver Notification List page loaded.");
        }


        // ============================================================
        // VERIFY PAGE LOAD TIME
        // ============================================================

        public async Task VerifyPageLoadTimeAsync()
        {
            long loadTime = await _page.EvaluateAsync<long>(
                @"() =>
                {
                    return Math.round(
                        performance.timing.domContentLoadedEventEnd -
                        performance.timing.navigationStart
                    );
                }");

            Console.WriteLine(
                $"Driver Notification List page load: {loadTime} ms");

            Assert.IsTrue(
                loadTime <= 20000,
                $"Driver Notification List page too slow: {loadTime} ms");
        }


        // ============================================================
        // VERIFY BREADCRUMBS AND HEADINGS
        // ============================================================

        public async Task VerifyStringsAndLinksAsync()
        {
            // Customer Management icon
            await CustomerManagementIcon.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.IsTrue(
                await CustomerManagementIcon.IsVisibleAsync(),
                "Customer Management icon is not displayed.");


            // Heading
            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.AreEqual(
                "Notifications",
                (await PageHeading.InnerTextAsync()).Trim());


            // Campaigns breadcrumb
            await CampaignsBreadcrumb.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.AreEqual(
                "Campaigns",
                (await CampaignsBreadcrumb.InnerTextAsync()).Trim());


            // Notifications breadcrumb
            await NotificationsBreadcrumb.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.AreEqual(
                "Notifications",
                (await NotificationsBreadcrumb.InnerTextAsync()).Trim());

            Console.WriteLine(
                "✔ Breadcrumbs and headings verified.");


            // ========================================================
            // NOTIFICATION TYPE DROPDOWN
            // ========================================================

            await NotificationTypeDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await NotificationTypeDropdown.SelectOptionAsync(
                new SelectOptionValue
                {
                    Label = "Promotions"
                });

            Console.WriteLine(
                "✔ Notification Type selected: Promotions");


            // ========================================================
            // SEARCH
            // ========================================================

            await SearchButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await SearchButton.ClickAsync();

            Console.WriteLine(
                "✔ Search button clicked.");


            // ========================================================
            // ADD NOTIFICATION BUTTON
            // ========================================================

            await AddNotificationButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.IsTrue(
                await AddNotificationButton.IsVisibleAsync(),
                "Add Notification button is not displayed.");

            Console.WriteLine(
                "✔ Add Notification button verified.");


            // ========================================================
            // SHOW ENTRIES = 50
            // ========================================================

            await ShowEntriesDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await ShowEntriesDropdown.SelectOptionAsync("50");

            Console.WriteLine(
                "✔ Show Entries set to 50.");


            // ========================================================
            // TABLE HEADERS
            // ========================================================

            var headers = _page.Locator("tr th");

            string[] expectedHeaders =
            {
                "Notification Type",
                "Title",
                "Start Date",
                "End Date"//,
                //" "
            };

            for (int i = 0; i < expectedHeaders.Length; i++)
            {
                var header = headers.Nth(i);

                await header.WaitForAsync(
                    new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 60000
                    });

                string actualHeader =
                    await header.InnerTextAsync();

                Assert.AreEqual(
                    expectedHeaders[i],
                    actualHeader,
                    $"Column header mismatch at position {i + 1}.");

                Console.WriteLine(
                    $"✔ Column {i + 1}: '{actualHeader}'");
            }


            // ========================================================
            // NOTIFICATION TYPE COLUMN
            // ========================================================

            await NotificationTypeColumn.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            string notificationTypeText =
                await NotificationTypeColumn.InnerTextAsync();

            Assert.IsFalse(
                string.IsNullOrEmpty(notificationTypeText),
                "Notification Type column data type value is not a string.");

            Console.WriteLine(
                "✔ Notification Type column Data Type value is a string.");


            // ========================================================
            // TITLE COLUMN
            // ========================================================

            await TitleColumn.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            string titleColumnText =
                await TitleColumn.InnerTextAsync();

            Assert.IsFalse(
                string.IsNullOrEmpty(titleColumnText),
                "Title column Data Type value is not a string.");

            Console.WriteLine(
                "✔ Title column Data Type value is a string.");


            // ========================================================
            // START DATE COLUMN
            // ========================================================

            await StartDateColumn.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            string startDateColumnText =
                await StartDateColumn.InnerTextAsync();

            Assert.IsFalse(
                string.IsNullOrEmpty(startDateColumnText),
                "Start Date column Data Type value is not a string.");

            Console.WriteLine(
                "✔ Start Date column Data Type value is a string.");


            // ========================================================
            // END DATE COLUMN
            // ========================================================

            await EndDateColumn.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            string endDateColumnText =
                await EndDateColumn.InnerTextAsync();

            Assert.IsFalse(
                string.IsNullOrEmpty(endDateColumnText),
                "End Date column Data Type value is not a string.");

            Console.WriteLine(
                "✔ End Date column Data Type value is a string.");


            // ========================================================
            // EDIT NOTIFICATION BUTTON
            // ========================================================

            await EditNotificationButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            string tagName =
                await EditNotificationButton.EvaluateAsync<string>(
                    "element => element.tagName");

            Assert.AreEqual(
                "A",
                tagName,
                "Edit Notification button icon is not a link.");

            Console.WriteLine(
                "✔ Edit Notification button icon is a link.");

            Console.WriteLine(
                "✔ Driver Notification List page validation completed.");
        }
    }
}