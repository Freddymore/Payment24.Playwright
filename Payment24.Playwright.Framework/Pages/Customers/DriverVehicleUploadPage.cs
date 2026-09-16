using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Payment24.Playwright.Framework.Pages.Customers
{
    public class DriverVehicleUploadPage
    {
        private readonly IPage _page;

        public DriverVehicleUploadPage(IPage page)
        {
            _page = page;
        }

        // ============================================================
        // PAGE LOCATORS
        // ============================================================

        private ILocator PageHeading =>
            _page.Locator("h2.heading");

        private ILocator MerchantGroupLabel =>
            _page.Locator("label.control-label").Nth(0);

        private ILocator CustomerLabel =>
            _page.Locator("label.control-label").Nth(1);

        private ILocator SendDriverVerificationCodeLabel =>
            _page.Locator("span[style='width:90%;']");

        private ILocator CustomerDropdown =>
            _page.Locator("#select2-SelectedFleetId-container");

        private ILocator Select2SearchField =>
            _page.Locator("input.select2-search__field").Last;

        private ILocator CustomerResults =>
            _page.Locator("#select2-SelectedFleetId-results li").First;

        private ILocator FileUpload =>
            _page.Locator("#img-input");

        private ILocator ViewButton =>
            _page.Locator("input[value='View']");

        private ILocator SendDriverPin =>
            _page.Locator("#SendDriverPin");

        private ILocator SubmitButton =>
            _page.Locator("input[type='submit']").Last;

        private ILocator SuccessMessage =>
            _page.Locator("#floating-top-right > div > div").First;


        // ============================================================
        // NAVIGATE TO DRIVER VEHICLE UPLOAD
        // ============================================================

        public async Task NavigateAsync()
        {
            await _page.GotoAsync(
                "https://admin-stage.payment24.co/DriverVehicleUpload",
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

            Console.WriteLine(
                "✔ Driver Vehicle Upload page loaded.");
        }


        // ============================================================
        // VERIFY PAGE LOAD TIME
        // ============================================================

        public async Task VerifyPageLoadTimeAsync()
        {
            long responseTime =
                await _page.EvaluateAsync<long>(
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
                $"Page {await _page.TitleAsync()} loading time is {responseTime} ms");

            Assert.IsTrue(
                responseTime <= 10000,
                $"Driver Vehicle Upload page loading time ({responseTime} ms) exceeds 10000 ms.");
        }


        // ============================================================
        // VERIFY PAGE DETAILS
        // ============================================================

        public async Task VerifyPageDetailsAsync()
        {
            await PageHeading.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Assert.AreEqual(
                "Driver Vehicle Upload",
                (await PageHeading.InnerTextAsync()).Trim());

            await MerchantGroupLabel.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

          
            /*Assert.AreEqual(
                "Customer",
                (await CustomerLabel.InnerTextAsync()).Trim());*/

            await SendDriverVerificationCodeLabel.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            Assert.AreEqual(
                "Send Driver Verification Code:",
                (await SendDriverVerificationCodeLabel.InnerTextAsync()).Trim());

            Console.WriteLine(
                "✔ Driver Vehicle Upload page fields verified.");
        }


        // ============================================================
        // SELECT CUSTOMER
        // ============================================================

        public async Task SelectCustomerAsync()
        {
            await CustomerDropdown.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            await CustomerDropdown.ClickAsync();

            await Select2SearchField.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            await Select2SearchField.FillAsync("DO NOT DELETE(BP HO)");

            await CustomerResults.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 30000
                });

            await CustomerResults.ClickAsync();

            Console.WriteLine(
                "✔ Customer selected: DO NOT DELETE(BP HO)");
        }


        // ============================================================
        // UPLOAD EXCEL FILE
        // ============================================================

        public async Task UploadDriverVehicleFileAsync(string filePath)
        {
            await FileUpload.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 30000
                });

            await FileUpload.SetInputFilesAsync(filePath);

            Console.WriteLine(
                $"✔ Driver Vehicle Upload template selected: {filePath}");
        }


        // ============================================================
        // CLICK VIEW
        // ============================================================

        public async Task ClickViewAsync()
        {
            await ViewButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 30000
                });

            await ViewButton.ClickAsync();

            // Wait for the uploaded data table to appear.
            await _page.Locator("tr th").First.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 60000
                });

            Console.WriteLine(
                "✔ View button clicked and uploaded driver data displayed.");
        }


        // ============================================================
        // VERIFY UPLOAD TABLE HEADERS
        // ============================================================

        public async Task VerifyUploadTableHeadersAsync()
        {
            var headers = _page.Locator("tr th");

            string[] expectedHeaders =
            {
                "EmployeeNumber",
                "Name",
                "Surname",
                "Cost Centre",
                "Email",
                "Cell Number",
                "License Code",
                "License Number",
                "License Expiry",
                "PDP Expiry",
                "ID Number",
                "Vehicle Registration Number",
                "Driver Group",
                "Driver Notification Type"
            };

            for (int i = 0; i < expectedHeaders.Length; i++)
            {
                var header = headers.Nth(i);

                await header.WaitForAsync(
                    new LocatorWaitForOptions
                    {
                        State = WaitForSelectorState.Visible,
                        Timeout = 30000
                    });

                string actualHeader =
                    (await header.InnerTextAsync()).Trim();

                Assert.AreEqual(
                    expectedHeaders[i],
                    actualHeader,
                    $"Column header mismatch at position {i + 1}.");

                Console.WriteLine(
                    $"✔ Column {i + 1}: {actualHeader}");
            }

            Console.WriteLine(
                "✔ All Driver Vehicle Upload table headers verified.");
        }


        // ============================================================
        // SEND DRIVER PIN
        // ============================================================

        public async Task SendDriverPinAsync()
        {
            await SendDriverPin.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 30000
                });

            Assert.IsTrue(
                await SendDriverPin.IsEnabledAsync(),
                "Send Driver PIN checkbox/button is not enabled.");

            await SendDriverPin.ClickAsync();

            Console.WriteLine(
                "✔ Send Driver PIN selected.");
        }


        // ============================================================
        // SUBMIT UPLOAD
        // ============================================================

        public async Task SubmitUploadAsync()
        {
            await SubmitButton.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 30000
                });

            await SubmitButton.ClickAsync();

            Console.WriteLine(
                "✔ Driver Vehicle Upload submitted.");
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

            string message = await SuccessMessage.InnerTextAsync();

            // Normalize whitespace/newlines
            string normalizedMessage =
                Regex.Replace(message, @"\s+", " ").Trim();

            string expectedMessage =
                "Success 1 drivers uploaded/updated.";

            string normalizedExpectedMessage =
                Regex.Replace(expectedMessage, @"\s+", " ").Trim();

            Assert.AreEqual(
                normalizedExpectedMessage,
                normalizedMessage,
                "Driver Vehicle Upload success message is incorrect.");

            Console.WriteLine("✔ Success message verified.");
            Console.WriteLine($"Success Message: {normalizedMessage}");
        }
    }
}