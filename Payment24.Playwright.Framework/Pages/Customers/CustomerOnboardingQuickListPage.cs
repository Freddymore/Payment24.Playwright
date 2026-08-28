using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class CustomerOnboardingQuickListPage : BasePage
{
    // =====================================================
    // URL
    // =====================================================

    private const string CustomerOnboardingQuickListPageUrl =
        "/CustomerOnboardingQuickList.aspx";


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
    // Customer
    // =====================================================

    private const string AddCustomerButton =
        "#cphBody_btnAdd";


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

    private const string GridHeaders =
        "#cphBody_gridCustomerList thead tr th";


    // =====================================================
    // Constructor
    // =====================================================

    public CustomerOnboardingQuickListPage(IPage page)
        : base(page)
    {
    }


    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await NavigateToAsync(CustomerOnboardingQuickListPageUrl);
    }


    // =====================================================
    // Page Loaded
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
            Page.Url.Contains("CustomerOnboardingQuickList.aspx"),
            "Customer Onboarding Quicklist page was not loaded.");

        Console.WriteLine(
            "✔ Customer Onboarding Quicklist page loaded.");
    }


    // =====================================================
    // Page Response Time
    // =====================================================

    public async Task VerifyPageLoadTimeAsync()
    {
        var loadTime = await Page.EvaluateAsync<int>(
            @"() => window.performance.timing.domContentLoadedEventEnd -
                    window.performance.timing.navigationStart");

        Assert.IsTrue(
            loadTime <= 10000,
            $"Customer Onboarding Quicklist page response time ({loadTime} ms) exceeds the threshold of 10000 ms.");

        Console.WriteLine(
            $"Customer Onboarding Quicklist page loaded in {loadTime} ms");
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

        await customerGroup.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        await customerGroup.SelectOptionAsync("6643");

        Console.WriteLine(
            "✔ Customer Group selected: 6643.");

        Assert.IsTrue(
            await Page.Locator(SearchTextBox).IsVisibleAsync(),
            "Customer search field is not visible.");

        Console.WriteLine(
            "✔ Customer search field verified.");

        var exactCheckbox =
            Page.Locator(ExactCheckbox);

        Assert.IsTrue(
            await exactCheckbox.IsVisibleAsync(),
            "Exact checkbox is not visible.");

        var exactLabel =
            (await Page.Locator(ExactCheckboxLabel).InnerTextAsync()).Trim();

        Assert.AreEqual(
            "Exact",
            exactLabel);

        Console.WriteLine(
            $"✔ Exact checkbox verified. Selected: {await exactCheckbox.IsCheckedAsync()}");

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
    // Export Options
    // =====================================================

    public async Task VerifyExportOptionsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== EXPORT OPTIONS ==========");

        var exportButton =
            Page.Locator(ExportDropdown);

        await exportButton.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

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
            "✔ PDF export option verified.");
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
    // Add Customer Button
    // =====================================================

    public async Task VerifyAddCustomerButtonAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CUSTOMER BUTTON ==========");

        var addCustomer =
            Page.Locator(AddCustomerButton);

        await addCustomer.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Attached,
                Timeout = 10000
            });

        Assert.IsTrue(
            await addCustomer.IsVisibleAsync(),
            "Add Customer button is not visible.");

        Console.WriteLine(
            "✔ Add Customer button verified.");
    }


    // =====================================================
    // Pagination
    // =====================================================

    public async Task VerifyPaginationAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== PAGINATION ==========");

        await Page.Locator(CustomerGrid).WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        var previous =
            Page.Locator(PreviousButton);

        // Match Selenium behaviour:
        // verify the element exists rather than requiring
        // the pagination control itself to be visible.
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

        await Page.Locator(GridSearch)
            .FillAsync("DO NOT DELETE");

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
                $"✔ {columnNames[i]} column value data type is a string. Value: {value}");
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
            "Loyalty Transactions "
        };

        foreach (var title in elementTitles)
        {
            var element =
                Page.Locator(
                    $"a[data-original-title='{title}']").First;

            await element.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = 10000
                });

            Assert.AreEqual(
                "a",
                await element.EvaluateAsync<string>(
                    "element => element.tagName.toLowerCase()"));

            Console.WriteLine(
                $"{title}button icon is a link.");
        }
    }


    // =====================================================
    // Complete Verification
    // =====================================================

    public async Task VerifyCustomerOnboardingQuickListPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "=================================================");
        Console.WriteLine(
            "VERIFYING CUSTOMER ONBOARDING QUICKLIST PAGE");
        Console.WriteLine(
            "=================================================");

        await VerifyPageLoadedAsync();

        await VerifyPageLoadTimeAsync();

        await VerifyMainPageDetailsAsync();

        await VerifySearchControlsAsync();

        await PerformSearchAsync();

        await VerifyExportOptionsAsync();

        await VerifyPdfExportAsync();

        await VerifyAddCustomerButtonAsync();

        await VerifyPaginationAsync();

        await VerifyGridHeadersAsync();

        await VerifyColumnDataTypesAsync();

        await VerifyCustomerActionLinksAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Customer Onboarding Quicklist page verified successfully.");
    }
}