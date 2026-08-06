using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class CustomerListPage : BasePage
{
    // =====================================================
    // URL
    // =====================================================

    private const string CustomerPageUrl = "/FleetList.aspx";

    // =====================================================
    // Header
    // =====================================================

    private const string Header = "h2.heading";

    // =====================================================
    // Search Section
    // =====================================================

    private const string CustomerGroupDropdown = "#cphBody_selCustomerGroup";

    private const string SearchFilterDropdown = "#cphBody_selSearchFilter";

    private const string SearchTextBox = "#cphBody_txtSearch";

    private const string ExactMatchCheckbox = "#cphBody_chkExact";

    private const string ExactMatchLabel = "label[for='cphBody_chkExact']";

    // =====================================================
    // Buttons
    // =====================================================

    private const string SearchButton = "#cphBody_btnSearch";

    private const string ExportButton = "#cphBody_btnExport";

    private const string ResetButton = "#cphBody_btnResetSearch";

    private const string NewB2CButton = "#cphBody_btnAddIndividual";

    private const string NewB2BButton = "#cphBody_btnAddFleet";

    // =====================================================
    // Customer Filters
    // =====================================================

    private const string AccountNumberTextBox = "#cphBody_txtMobileNumber";

    private const string ReferenceNumberDropdown = "#select2-cphBody_ddlReferenceNumber-container";

    private const string AccountNameTextBox = "#cphBody_txtFleetName";

    private const string RegistrationNumberTextBox = "#cphBody_txtRegistrationNumber";

    private const string CustomerStatusDropdown = "#select2-cphBody_selCustomerStatus-container";

    private const string CustomerTypeDropdown = "#select2-cphBody_selCustomerType-container";

    private const string CategoryDropdown = "#select2-cphBody_selCustomerCategory-container";

    private const string DivisionDropdown = "#select2-cphBody_selDivision-container";

    private const string StartDate = "#cphBody_txtStartDate";

    private const string EndDate = "#cphBody_txtEndDate";

    // =====================================================
    // Grid
    // =====================================================

    private const string GridFilter = "#cphBody_gridThirdParty_filter";

    private const string GridFilterInput = "#cphBody_gridThirdParty_filter input";

    private const string GridHeaders = "#cphBody_gridThirdParty thead th";

    public CustomerListPage(IPage page)
        : base(page)
    {
    }

    // =====================================================
    // Grid Data
    // =====================================================

    private const string GridRows =
        "#cphBody_gridThirdParty tbody tr";

    private const string CustomerIdCell =
        "#cphBody_gridThirdParty tbody tr:first-child td:nth-child(1)";

    private const string AccountNumberCell =
        "#cphBody_gridThirdParty tbody tr:first-child td:nth-child(2)";

    private const string ReferenceNumberCell =
        "#cphBody_gridThirdParty tbody tr:first-child td:nth-child(3)";

    private const string CreditLimitCell =
        "#cphBody_gridThirdParty tbody tr:first-child td:nth-child(4)";

    private const string AvailableBalanceCell =
        "#cphBody_gridThirdParty tbody tr:first-child td:nth-child(5)";

    private const string OpenVouchersCell =
        "#cphBody_gridThirdParty tbody tr:first-child td:nth-child(6)";

    private const string RemainingBalanceCell =
        "#cphBody_gridThirdParty tbody tr:first-child td:nth-child(7)";

    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await NavigateToAsync(CustomerPageUrl);
    }

    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyPageLoadedAsync()
    {
        await WaitForPageLoadAsync();

        Assert.IsTrue(
            Page.Url.Contains("FleetList.aspx"),
            "Customer List page was not loaded.");

        Console.WriteLine("✔ Customer List page loaded.");
    }

    public async Task VerifyHeaderAsync()
    {
        var heading = await GetTextAsync(Header);

        Assert.AreEqual(
            "Customer Management",
            heading);

        Console.WriteLine("✔ Customer Management heading verified.");
    }

    // =====================================================
    // Search Section
    // =====================================================

    public async Task VerifySearchSectionAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== SEARCH SECTION ==========");

        await SelectByLabelAsync(CustomerGroupDropdown, "All Customer Groups");
        Console.WriteLine("✔ Customer Group dropdown");

        await SelectByLabelAsync(SearchFilterDropdown, "Contract Number");
        Console.WriteLine("✔ Search Filter dropdown");

        await FillAsync(SearchTextBox, "IMPL795333");
        Console.WriteLine("✔ Search textbox");

        Assert.IsFalse(
            await Page.Locator(ExactMatchCheckbox).IsCheckedAsync());

        Console.WriteLine("✔ Exact checkbox");

        Assert.AreEqual(
            "Exact",
            await GetTextAsync(ExactMatchLabel));

        Console.WriteLine("✔ Exact label");

        Console.WriteLine("✔ Search section verified.");
    }

    // =====================================================
    // Buttons
    // =====================================================

    public async Task VerifyButtonsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== BUTTONS ==========");

        Assert.IsTrue(await IsVisibleAsync(SearchButton));
        Console.WriteLine("✔ Search");

        Assert.IsTrue(await IsVisibleAsync(ExportButton));
        Console.WriteLine("✔ Export");

        Assert.IsTrue(await IsVisibleAsync(ResetButton));
        Console.WriteLine("✔ Reset");

        Assert.IsTrue(await IsVisibleAsync(NewB2CButton));
        Console.WriteLine("✔ New B2C");

        Assert.IsTrue(await IsVisibleAsync(NewB2BButton));
        Console.WriteLine("✔ New B2B");
    }

    // =====================================================
    // Customer Filters
    // =====================================================

    public async Task VerifyCustomerFiltersAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CUSTOMER FILTERS ==========");

        Assert.IsTrue(await IsVisibleAsync(AccountNumberTextBox));
        Console.WriteLine("✔ Account Number");

        Assert.IsTrue(await IsVisibleAsync(ReferenceNumberDropdown));
        Console.WriteLine("✔ Reference Number");

        Assert.IsTrue(await IsVisibleAsync(AccountNameTextBox));
        Console.WriteLine("✔ Account Name");

        Assert.IsTrue(await IsVisibleAsync(RegistrationNumberTextBox));
        Console.WriteLine("✔ Registration Number");

        Assert.IsTrue(await IsVisibleAsync(CustomerStatusDropdown));
        Console.WriteLine("✔ Customer Status");

        Assert.IsTrue(await IsVisibleAsync(CustomerTypeDropdown));
        Console.WriteLine("✔ Customer Type");

        Assert.IsTrue(await IsVisibleAsync(CategoryDropdown));
        Console.WriteLine("✔ Category");

        Assert.IsTrue(await IsVisibleAsync(DivisionDropdown));
        Console.WriteLine("✔ Division");

        Assert.IsTrue(await IsVisibleAsync(StartDate));
        Console.WriteLine("✔ Start Date");

        Assert.IsTrue(await IsVisibleAsync(EndDate));
        Console.WriteLine("✔ End Date");
    }

    // =====================================================
    // Search Customer
    // =====================================================

    public async Task SearchCustomerAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== SEARCH CUSTOMER ==========");

        await ClickAsync(SearchButton);

        // Wait until the grid filter is displayed
        await Page.Locator(GridFilter).WaitForAsync();

        Console.WriteLine("✔ Customer search completed.");
    }

    // =====================================================
    // Grid
    // =====================================================

    public async Task VerifyGridFilterAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GRID FILTER ==========");

        var filter = Page.Locator(GridFilter);

        Assert.IsTrue(
            await filter.IsVisibleAsync(),
            "Grid filter was not displayed.");

        var text = await filter.InnerTextAsync();

        Assert.IsTrue(
            text.Contains("Filter:"),
            "Grid Filter label incorrect.");

        Console.WriteLine("✔ Grid Filter");

        Assert.IsTrue(
            await Page.Locator(GridFilterInput).IsVisibleAsync(),
            "Grid Filter textbox not displayed.");

        Console.WriteLine("✔ Grid Filter textbox");
    }

    public async Task VerifyGridHeadersAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GRID HEADERS ==========");

        string[] expectedHeaders =
        {
            "Customer",
            "Account #",
            "Reference Number",
            "Credit Limit",
            "Avl. Balance",
            "Open Vouchers",
            "Remaining Balance",
            ""
        };

        var headers = Page.Locator(GridHeaders);

        for (int i = 0; i < expectedHeaders.Length; i++)
        {
            var actual = (await headers.Nth(i).InnerTextAsync()).Trim();

            Assert.AreEqual(
                expectedHeaders[i],
                actual,
                $"Grid Header mismatch at column {i + 1}");

            Console.WriteLine($"✔ {actual}");
        }
    }

    public async Task VerifyGridDataAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GRID DATA ==========");

        var rowCount = await GetCountAsync(GridRows);

        Assert.IsTrue(
            rowCount > 0,
            "No customer records were returned.");

        Console.WriteLine($"✔ {rowCount} customer record(s) returned.");

        // Customer ID
        var customerId =
            (await GetTextAsync(CustomerIdCell)).Trim();

        Assert.IsTrue(
            int.TryParse(customerId, out _),
            $"Customer ID should be numeric but was '{customerId}'.");

        Console.WriteLine($"✔ Customer ID : {customerId}");

        // Account Number
        var accountNumber =
            (await GetTextAsync(AccountNumberCell)).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(accountNumber),
            "Account Number is empty.");

        Console.WriteLine($"✔ Account Number : {accountNumber}");

        // Reference Number
        var reference =
            (await GetTextAsync(ReferenceNumberCell)).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(reference),
            "Reference Number is empty.");

        Console.WriteLine($"✔ Reference Number : {reference}");

        // Credit Limit
        var creditLimit =
            (await GetTextAsync(CreditLimitCell))
            .Replace(",", "")
            .Trim();

        Assert.IsTrue(
            decimal.TryParse(creditLimit, out _),
            "Credit Limit is not numeric.");

        Console.WriteLine($"✔ Credit Limit : {creditLimit}");

        // Available Balance
        var available =
            (await GetTextAsync(AvailableBalanceCell))
            .Replace(",", "")
            .Trim();

        Assert.IsTrue(
            decimal.TryParse(available, out _),
            "Available Balance is not numeric.");

        Console.WriteLine($"✔ Available Balance : {available}");

        // Open Vouchers
        var vouchers =
            (await GetTextAsync(OpenVouchersCell))
            .Replace(",", "")
            .Trim();

        Assert.IsTrue(
            decimal.TryParse(vouchers, out _),
            "Open Vouchers is not numeric.");

        Console.WriteLine($"✔ Open Vouchers : {vouchers}");

        // Remaining Balance
        var remaining =
            (await GetTextAsync(RemainingBalanceCell))
            .Replace(",", "")
            .Trim();

        Assert.IsTrue(
            decimal.TryParse(remaining, out _),
            "Remaining Balance is not numeric.");

        Console.WriteLine($"✔ Remaining Balance : {remaining}");
    }

    public async Task VerifyActionIconsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== ACTION ICONS ==========");

        string[] icons =
        {
        "Customer Profile",
        "Manage Vehicles",
        "Manage Drivers",
        "Link Vehicles to Driver",
        "Customer Wallet",
        "View Statement",
        "Departments",
        "Transactions",
        "Portal Users",
        "Merchant Discount",
        "Head Office Level Discounts",
        "Transfer"
    };

        foreach (var icon in icons)
        {
            var locator = Page.Locator(
                $"a[data-original-title='{icon}']").First;

            Assert.IsTrue(
                await locator.IsVisibleAsync(),
                $"{icon} icon was not displayed.");

            Console.WriteLine($"✔ {icon}");
        }
    }

    // =====================================================
    // Complete Page Verification
    // =====================================================

    public async Task VerifyCustomerPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=====================================");
        Console.WriteLine("VERIFYING CUSTOMER MANAGEMENT PAGE");
        Console.WriteLine("=====================================");

        await VerifyPageLoadedAsync();

        await VerifyHeaderAsync();

        await VerifySearchSectionAsync();

        await VerifyButtonsAsync();

        await VerifyCustomerFiltersAsync();

        await SearchCustomerAsync();

        await VerifyGridFilterAsync();

        await VerifyGridHeadersAsync();

        await VerifyGridDataAsync();

        await VerifyActionIconsAsync();

        Console.WriteLine();
        Console.WriteLine("✔ Customer Management page verified.");
    }
}