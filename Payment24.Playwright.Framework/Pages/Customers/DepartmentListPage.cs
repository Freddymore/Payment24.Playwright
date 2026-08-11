using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class DepartmentListPage : BasePage
{
    // =====================================================
    // URL
    // =====================================================

    private const string DepartmentPageUrl =
        "/DepartmentList.aspx";

    // =====================================================
    // Header
    // =====================================================

    private const string Heading =
        "h2.heading";

    // =====================================================
    // Search Section
    // =====================================================

    private const string CustomerDropdown =
        "#select2-cphBody_selFleetMerchant-container";

    private const string CustomerSearchInput =
        ".select2-search__field";

    private const string SearchTextbox =
        "#cphBody_txtSearch";

    private const string StatusDropdown =
        "#select2-cphBody_selIsActive-container";

    private const string SearchButton =
        "#cphBody_btnSearch";

    // =====================================================
    // Grid Filter
    // =====================================================

    private const string GridFilter =
        "#cphBody_gridDepartment_filter";

    private const string GridFilterTextbox =
        "#cphBody_gridDepartment_filter input";

    // =====================================================
    // Grid Headers
    // =====================================================

    private readonly string[] ExpectedHeaders =
    {
        "Id",
        "Account Name",
        "Departments",
        "Cost Centre",
        "Region",
        "Credit Limit",
        "Avl. Balance",
        "Open Coupons",
        ""
    };

    // =====================================================
    // Grid Data
    // =====================================================

    private const string GridRows =
        "#cphBody_gridDepartment tbody tr";

    private const string IdCell =
        "#cphBody_gridDepartment tbody tr:first-child td:nth-child(1)";

    private const string AccountNameCell =
        "#cphBody_gridDepartment tbody tr:first-child td:nth-child(2)";

    private const string DepartmentCell =
        "#cphBody_gridDepartment tbody tr:first-child td:nth-child(3)";

    private const string CostCentreCell =
        "#cphBody_gridDepartment tbody tr:first-child td:nth-child(4)";

    private const string RegionCell =
        "#cphBody_gridDepartment tbody tr:first-child td:nth-child(5)";

    private const string CreditLimitCell =
        "#cphBody_gridDepartment tbody tr:first-child td:nth-child(6)";

    private const string AvailableBalanceCell =
        "#cphBody_gridDepartment tbody tr:first-child td:nth-child(7)";

    private const string OpenCouponsCell =
        "#cphBody_gridDepartment tbody tr:first-child td:nth-child(8)";

    // =====================================================
    // Constructor
    // =====================================================

    public DepartmentListPage(IPage page)
        : base(page)
    {
    }

    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await NavigateToAsync(DepartmentPageUrl);
    }

    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyPageLoadedAsync()
    {
        await WaitForPageLoadAsync();

        Assert.IsTrue(
            Page.Url.Contains("DepartmentList.aspx"),
            "Department page was not loaded.");

        Console.WriteLine("✔ Department page loaded.");
    }

    public async Task VerifyHeadingAsync()
    {
        var heading = await GetTextAsync(Heading);

        Assert.AreEqual(
            "Departments",
            heading);

        Console.WriteLine("✔ Departments heading verified.");
    }

    // =====================================================
    // Search Section
    // =====================================================

    public async Task VerifySearchSectionAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== SEARCH SECTION ==========");

        Assert.IsTrue(await IsVisibleAsync(CustomerDropdown));
        Console.WriteLine("✔ Customer dropdown");

        Assert.IsTrue(await IsVisibleAsync(SearchTextbox));
        Console.WriteLine("✔ Search textbox");

        Assert.IsTrue(await IsVisibleAsync(StatusDropdown));
        Console.WriteLine("✔ Status dropdown");

        Console.WriteLine("✔ Search section verified.");
    }

    // =====================================================
    // Search
    // =====================================================

        public async Task SearchDepartmentAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== SEARCH ==========");

        // Select customer
        await ClickAsync(CustomerDropdown);

        await FillAsync(CustomerSearchInput, "DO NOT DELETE");

        var firstCustomerResult = Page.Locator(
            "ul.select2-results__options li"
        ).First;

        await firstCustomerResult.WaitForAsync();
        await firstCustomerResult.ClickAsync();

        // Select status
        await ClickAsync(StatusDropdown);

        var firstStatusOption = Page.Locator(
            "#select2-cphBody_selIsActive-results li"
        ).First;

        await firstStatusOption.WaitForAsync();
        await firstStatusOption.ClickAsync();

        // Click Search
        await ClickAsync(SearchButton);

        Console.WriteLine("✔ Department search completed.");
    }


    // =====================================================
    // Grid Filter
    // =====================================================

   
public async Task VerifyGridFilterAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GRID FILTER ==========");

        var gridSearchInput = Page.Locator("input[type='search']").Last;

        await gridSearchInput.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });

        Assert.IsTrue(
            await gridSearchInput.IsVisibleAsync(),
            "Grid search textbox is not visible.");

        Console.WriteLine("✔ Grid Filter textbox");

        await gridSearchInput.FillAsync("1531009");

        Console.WriteLine("✔ Department ID filter applied: 1531009");
    }


    // =====================================================
    // Grid Headers
    // =====================================================

       public async Task VerifyGridHeadersAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GRID HEADERS ==========");

        var tables = Page.Locator("table");

        Console.WriteLine($"Tables found: {await tables.CountAsync()}");

        var headers = Page.Locator("tr th");

        Console.WriteLine($"Header cells found: {await headers.CountAsync()}");

        var headerTexts = await headers.AllInnerTextsAsync();

        foreach (var header in headerTexts)
        {
            Console.WriteLine($"Header: [{header.Trim()}]");
        }

        Assert.AreEqual(
            ExpectedHeaders.Length,
            headerTexts.Count,
            "Department grid header count mismatch.");

        for (int i = 0; i < ExpectedHeaders.Length; i++)
        {
            Assert.AreEqual(
                ExpectedHeaders[i],
                headerTexts[i].Trim());

            Console.WriteLine($"✔ {headerTexts[i].Trim()}");
        }
    }

    // =====================================================
    // Grid Data
    // =====================================================

    public async Task VerifyGridDataAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GRID DATA ==========");

        // Get first row
        var firstRow = Page.Locator("table tbody tr").First;

        await firstRow.WaitForAsync();

        var cells = firstRow.Locator("td");

        Assert.IsTrue(
            await cells.CountAsync() >= 8,
            "Department grid does not contain enough data columns.");

        // Id - Number
        var id = (await cells.Nth(0).InnerTextAsync()).Trim();

        Assert.IsTrue(
            int.TryParse(id, out _),
            $"Id data type is not a number. Actual value: '{id}'");

        Console.WriteLine($"✔ Id data type is a number. Value: {id}");

        // Account Name - String
        var accountName = (await cells.Nth(1).InnerTextAsync()).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(accountName),
            "Account Name is empty.");

        Console.WriteLine($"✔ Account Name data type is a string. Value: {accountName}");

        // Departments - String
        var departments = (await cells.Nth(2).InnerTextAsync()).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(departments),
            "Departments is empty.");

        Console.WriteLine($"✔ Departments data type is a string. Value: {departments}");

        // Cost Centre - String
        var costCentre = (await cells.Nth(3).InnerTextAsync()).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(costCentre),
            "Cost Centre is empty.");

        Console.WriteLine($"✔ Cost Centre data type is a string. Value: {costCentre}");

        // Region - String
        var region = (await cells.Nth(4).InnerTextAsync()).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(region),
            "Region is empty.");

        Console.WriteLine($"✔ Region data type is a string. Value: {region}");

        // Credit Limit - Number
        var creditLimit = (await cells.Nth(5).InnerTextAsync()).Trim();

        Assert.IsTrue(
            decimal.TryParse(creditLimit.Replace(",", ""), out _),
            $"Credit Limit data type is not a number. Actual value: '{creditLimit}'");

        Console.WriteLine($"✔ Credit Limit data type is a number. Value: {creditLimit}");

        // Available Balance - Number
        var availableBalance = (await cells.Nth(6).InnerTextAsync()).Trim();

        Assert.IsTrue(
            decimal.TryParse(availableBalance.Replace(",", ""), out _),
            $"Avl. Balance data type is not a number. Actual value: '{availableBalance}'");

        Console.WriteLine($"✔ Avl. Balance data type is a number. Value: {availableBalance}");

        // Open Coupons - Number
        var openCoupons = (await cells.Nth(7).InnerTextAsync()).Trim();

        Assert.IsTrue(
            decimal.TryParse(openCoupons.Replace(",", ""), out _),
            $"Open Coupons data type is not a number. Actual value: '{openCoupons}'");

        Console.WriteLine($"✔ Open Coupons data type is a number. Value: {openCoupons}");
    }

    // =====================================================
    // Action Icons
    // =====================================================

    public async Task VerifyActionIconsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== ACTION ICONS ==========");

        string[] icons =
        {
            "Department Details",
            "Manage drivers",
            "Manage Vehicles",
            "Transactions",
            "Department Discount",
            "Head Office Level Discounts"
        };

        foreach (var icon in icons)
        {
            var locator = Page.Locator($"a[data-original-title='{icon}']").First;

            Assert.IsTrue(await locator.IsVisibleAsync());

            Console.WriteLine($"✔ {icon}");
        }
    }

    // =====================================================
    // Complete Verification
    // =====================================================

    public async Task VerifyDepartmentPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=====================================");
        Console.WriteLine("VERIFYING DEPARTMENTS PAGE");
        Console.WriteLine("=====================================");

        await VerifyPageLoadedAsync();

        await VerifyHeadingAsync();

        await VerifySearchSectionAsync();

        await SearchDepartmentAsync();

        await VerifyGridFilterAsync();

        await VerifyGridHeadersAsync();

        await VerifyGridDataAsync();

        await VerifyActionIconsAsync();

        Console.WriteLine();
        Console.WriteLine("✔ Departments page verified.");
    }
}