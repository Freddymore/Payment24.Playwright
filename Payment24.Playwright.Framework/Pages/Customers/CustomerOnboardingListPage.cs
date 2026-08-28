using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class CustomerOnboardingListPage : BasePage
{
    // =====================================================
    // URL
    // =====================================================

    private const string CustomerOnboardingListPageUrl =
        "/CustomerOnboardingList.aspx";


    // =====================================================
    // Main Page Elements
    // =====================================================

    private const string CustomerManagementIcon =
        "img[src='images/icons/customer_management_icon@2x.png']";

    private const string PageHeading =
        "h2.heading";

    private const string CustomersBreadcrumb =
        "a.breadcrumb.breadbtn2";


    // =====================================================
    // Search / Filter
    // =====================================================

    private const string CustomerGroupDropdown =
        "#cphBody_selCustomerGroup";

    private const string SearchTextBox =
        "#cphBody_txtSearch";

    private const string ExactCheckbox =
        "#cphBody_chkExact";

    private const string ExactCheckboxLabel =
        "label[for='cphBody_chkExact']";

    private const string SearchButton =
        "input[value='Search']";


    // =====================================================
    // Export
    // =====================================================

    private const string ExportDropdown =
        "button[data-toggle='dropdown']";

    private const string ExportHeader =
        "li.dropdown-header";

    private const string ExportExcel =
        "#cphBody_excelExport";

    private const string ExportPdf =
        "#cphBody_pdfExport";


    // =====================================================
    // Customer Buttons
    // =====================================================

    private const string AddNewLoyaltyCustomer =
        "#btnAddNewLoyaltyCustomer";

    private const string AddBulkLoyaltyCustomer =
        "#cphBody_btnAddBulkLoyaltyCustomer";


    // =====================================================
    // Customer Grid
    // =====================================================

    private const string CustomerGrid =
        "#cphBody_gridCustomerList";

    private const string PreviousButton =
        "#cphBody_gridCustomerList_previous";

    private const string NextButton =
        "#cphBody_gridCustomerList_next";

    private const string ShowEntriesDropdown =
        "select[name='cphBody_gridCustomerList_length']";

    private const string GridFilter =
        "#cphBody_gridCustomerList_filter";

    private const string GridSearch =
        "#cphBody_gridCustomerList_filter input[type='search']";


    // =====================================================
    // Success / General
    // =====================================================

    private const string GridHeaders =
        "#cphBody_gridCustomerList thead tr th";


    // =====================================================
    // Constructor
    // =====================================================

    public CustomerOnboardingListPage(IPage page)
        : base(page)
    {
    }


    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await NavigateToAsync(CustomerOnboardingListPageUrl);
    }


    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyPageLoadedAsync()
    {
        await WaitForPageLoadAsync();

        await Page.Locator(PageHeading).WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        Assert.IsTrue(
            Page.Url.Contains("CustomerOnboardingList.aspx"),
            "Customer Onboarding List page was not loaded.");

        Console.WriteLine(
            "✔ Customer Onboarding List page loaded.");
    }


    public async Task VerifyPageLoadTimeAsync()
    {
        var loadTime = await Page.EvaluateAsync<int>(
            @"() => window.performance.timing.domContentLoadedEventEnd -
                    window.performance.timing.navigationStart");

        Assert.IsTrue(
            loadTime <= 10000,
            $"Customer Onboarding List page response time {loadTime} ms exceeds threshold of 10000 ms.");

        Console.WriteLine(
            $"Customer Onboarding List page loaded in {loadTime} ms");
    }


    // =====================================================
    // Main Page Details
    // =====================================================

    public async Task VerifyMainPageDetailsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== MAIN PAGE DETAILS ==========");

        Assert.IsTrue(
            await Page.Locator(CustomerManagementIcon).IsVisibleAsync(),
            "Customer Management icon is not visible.");

        Console.WriteLine(
            "✔ Customer Management icon verified.");

        var heading =
            (await Page.Locator(PageHeading).InnerTextAsync()).Trim();

        Assert.AreEqual(
            "Loyalty Customers",
            heading);

        Console.WriteLine(
            "✔ Loyalty Customers heading verified.");

        var breadcrumb =
            (await Page.Locator(CustomersBreadcrumb).InnerTextAsync()).Trim();

        Assert.AreEqual(
            "Customers",
            breadcrumb);

        Console.WriteLine(
            "✔ Customers breadcrumb verified.");
    }


    // =====================================================
    // Search Controls
    // =====================================================

    public async Task VerifySearchControlsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== SEARCH CONTROLS ==========");

        var customerGroup =
            Page.Locator(CustomerGroupDropdown);

        Assert.IsTrue(
            await customerGroup.IsVisibleAsync(),
            "Customer Group dropdown is not visible.");

        await customerGroup.SelectOptionAsync("6643");

        Console.WriteLine(
            "✔ Customer Group selected: 6643.");

        Assert.IsTrue(
            await Page.Locator(SearchTextBox).IsVisibleAsync(),
            "Customer search field is not visible.");

        Console.WriteLine(
            "✔ Customer search field verified.");

        Assert.IsTrue(
            await Page.Locator(ExactCheckbox).IsVisibleAsync(),
            "Exact checkbox is not visible.");

        var exactLabel =
            (await Page.Locator(ExactCheckboxLabel).InnerTextAsync()).Trim();

        Assert.AreEqual(
            "Exact",
            exactLabel);

        Console.WriteLine(
            $"✔ Exact checkbox verified. Selected: {await Page.Locator(ExactCheckbox).IsCheckedAsync()}");

        Assert.IsTrue(
            await Page.Locator(SearchButton).IsVisibleAsync(),
            "Search button is not visible.");

        Console.WriteLine(
            "✔ Search button verified.");
    }


    // =====================================================
    // Search
    // =====================================================

    public async Task PerformSearchAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== SEARCH ==========");

        await Page.Locator(SearchButton).ClickAsync();

        await WaitForPageLoadAsync();

        Console.WriteLine(
            "✔ Customer search completed.");
    }


    // =====================================================
    // Export
    // =====================================================

    public async Task VerifyExportOptionsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== EXPORT OPTIONS ==========");

        var exportButton =
            Page.Locator(ExportDropdown);

        Assert.IsTrue(
            await exportButton.IsVisibleAsync(),
            "Export dropdown button is not visible.");

        await exportButton.ClickAsync();

        var exportHeader =
            Page.Locator(ExportHeader);

        await exportHeader.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        Assert.AreEqual(
            "EXPORT TO:",
            (await exportHeader.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Export dropdown verified.");

        Assert.AreEqual(
            "Excel",
            (await Page.Locator(ExportExcel).InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Excel export option verified.");

        Assert.AreEqual(
            "Pdf",
            (await Page.Locator(ExportPdf).InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Pdf export option verified.");
    }


    // =====================================================
    // PDF Export
    // =====================================================

    public async Task VerifyPdfExportAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== PDF EXPORT ==========");

        await Page.Locator(ExportPdf).ClickAsync();

        Console.WriteLine(
            "✔ PDF export action executed.");
    }


    // =====================================================
    // Customer Buttons
    // =====================================================

    public async Task VerifyCustomerButtonsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CUSTOMER BUTTONS ==========");

        Assert.IsTrue(
            await Page.Locator(AddNewLoyaltyCustomer).IsVisibleAsync(),
            "Add New Loyalty Customer button is not visible.");

        Console.WriteLine(
            "✔ Add New Loyalty Customer button verified.");

        Assert.IsTrue(
            await Page.Locator(AddBulkLoyaltyCustomer).IsVisibleAsync(),
            "Add Bulk Loyalty Customer button is not visible.");

        Console.WriteLine(
            "✔ Add Bulk Loyalty Customer button verified.");
    }


    // =====================================================
    // Pagination
    // =====================================================

    public async Task VerifyPaginationAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== PAGINATION ==========");

        // Wait for the customer grid to finish rendering
        await Page.Locator(CustomerGrid).WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        var previous =
            Page.Locator(PreviousButton);

        // Selenium only verified that the element exists.
        // We will do the same here.
        await previous.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Attached,
                Timeout = 15000
            });

        var previousClass =
            await previous.GetAttributeAsync("class");

        Assert.IsTrue(
            previousClass?.Contains("disabled") == true,
            "Previous pagination button should be disabled.");

        Console.WriteLine(
            "✔ Previous pagination button is disabled.");

        var next =
            Page.Locator(NextButton);

        await next.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Attached,
                Timeout = 15000
            });

        Console.WriteLine(
            "✔ Next pagination button verified.");


        // -------------------------------------------------
        // Show 100 entries
        // -------------------------------------------------

        var entriesDropdown =
            Page.Locator(ShowEntriesDropdown);

        await entriesDropdown.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        await entriesDropdown.SelectOptionAsync(
            new SelectOptionValue
            {
                Label = "100"
            });

        Console.WriteLine(
            "✔ Show entries changed to 100.");


        // -------------------------------------------------
        // Grid Filter
        // -------------------------------------------------

        var filter =
            Page.Locator(GridFilter);

        await filter.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        Assert.AreEqual(
            "Filter",
            (await filter.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Grid Filter verified.");

        var gridSearch =
            Page.Locator(GridSearch);

        await gridSearch.FillAsync("DO NOT DELETE");

        Console.WriteLine(
            "✔ Grid filter value entered: DO NOT DELETE");
    }


    // =====================================================
    // Grid Headers
    // =====================================================

    public async Task VerifyGridHeadersAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GRID HEADERS ==========");

        string[] expectedHeaders =
        {
            "Account #",
            "Name",
            "Surname",
            "Customer Group",
            "ID",
            "Cell Nr",
            "Date of Registration",
            "Balance",
            " "
        };

        var headers =
            Page.Locator(GridHeaders);

        var count =
            await headers.CountAsync();

        Assert.AreEqual(
            expectedHeaders.Length,
            count,
            "Unexpected number of grid headers.");

        for (int i = 0; i < expectedHeaders.Length; i++)
        {
            var actual =
                (await headers.Nth(i).InnerTextAsync()).Trim();

            Assert.AreEqual(
                expectedHeaders[i].Trim(),
                actual,
                $"Grid header at position {i + 1} is incorrect.");

            Console.WriteLine(
                $"✔ Header {i + 1}: {actual}");
        }
    }


    // =====================================================
    // Grid Data Types
    // =====================================================

    public async Task VerifyColumnDataTypesAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GRID DATA TYPES ==========");

        string[] columnNames =
        {
            "Account Number",
            "Name",
            "Surname",
            "Customer Group",
            "ID",
            "Cell Nr",
            "Date of Registration",
            "Balance"
        };

        for (int i = 0; i < columnNames.Length; i++)
        {
            var cell =
                Page.Locator(
                    $"#cphBody_gridCustomerList tbody tr:first-child td:nth-child({i + 1})");

            await cell.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 10000
                });

            var value =
                (await cell.InnerTextAsync()).Trim();

            Assert.IsFalse(
                string.IsNullOrEmpty(value),
                $"{columnNames[i]} does not contain data.");

            Console.WriteLine(
                $"✔ {columnNames[i]} data type is a string. Value: {value}");
        }
    }


    // =====================================================
    // Customer Action Links
    // =====================================================

    public async Task VerifyCustomerActionLinksAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CUSTOMER ACTION LINKS ==========");

        string[] elementTitles =
        {
            "Customer Profile",
            " Transactions ",
            "Manage Vehicles",
            "Manage Drivers",
            "Link Vehicles to Driver",
            " Department Details ",
            " Portal Users ",
            "Transfer",
            " View Statement"
        };

        foreach (var title in elementTitles)
        {
            var element =
                Page.Locator(
                    $"a[data-original-title='{title}']").First;

            Assert.IsTrue(
                await element.IsVisibleAsync(),
                $"Customer action link '{title}' is not visible.");

            Assert.AreEqual(
                "a",
                await element.EvaluateAsync<string>(
                    "element => element.tagName.toLowerCase()"));

            Console.WriteLine(
                $"✔ {title} button icon is a link.");
        }
    }


    // =====================================================
    // Complete Verification
    // =====================================================

    public async Task VerifyCustomerOnboardingListPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("VERIFYING CUSTOMER ONBOARDING LIST PAGE");
        Console.WriteLine("==============================================");

        await VerifyPageLoadedAsync();

        await VerifyPageLoadTimeAsync();

        await VerifyMainPageDetailsAsync();

        await VerifySearchControlsAsync();

        await PerformSearchAsync();

        await VerifyExportOptionsAsync();

        await VerifyPdfExportAsync();

        await VerifyCustomerButtonsAsync();

        await VerifyPaginationAsync();

        await VerifyGridHeadersAsync();

        await VerifyColumnDataTypesAsync();

        await VerifyCustomerActionLinksAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Customer Onboarding List page verified successfully.");
    }
}