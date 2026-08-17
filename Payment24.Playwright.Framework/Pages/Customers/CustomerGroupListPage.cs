using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class CustomerGroupListPage : BasePage
{
    // =====================================================
    // Page Locators
    // =====================================================

    private const string CustomerManagementIcon =
        "img[src='images/icons/customer_management_icon@2x.png']";

    private const string PageHeading =
        "h2.heading";

    private const string SettingsBreadcrumb =
        "a.breadcrumb.breadbtn2";

    private const string SearchInput =
        "#cphBody_txtSearch";

    private const string SearchButton =
        "#cphBody_btnSearch";

    private const string ExportButton =
        "button[data-toggle='dropdown']";

    private const string AddCustomerGroupButton =
        "#cphBody_btnAddCustomerGroup";

    private const string ShowEntriesDropdown =
        "select[name='cphBody_grvCustomerGroup_length']";

    private const string FilterContainer =
        "#cphBody_grvCustomerGroup_filter";

    private const string DataTable =
        "#cphBody_grvCustomerGroup";

    private const string TableSearchInput =
        "#cphBody_grvCustomerGroup_filter input[type='search']";

    private const string GroupDetailsAction =
        "a[data-original-title='Group Details']";


    // =====================================================
    // Constructor
    // =====================================================

    public CustomerGroupListPage(IPage page)
        : base(page)
    {
    }


    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await Page.GotoAsync(
            "https://admin-stage.payment24.co/CustomerGroupList.aspx");

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);
    }


    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyCustomerGroupListPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=====================================");
        Console.WriteLine("VERIFYING CUSTOMER GROUP LIST PAGE");
        Console.WriteLine("=====================================");


        // Customer Management icon
        Assert.IsTrue(
            await Page.Locator(CustomerManagementIcon).IsVisibleAsync(),
            "Customer Management icon is not visible.");

        Console.WriteLine(
            "✔ Customer Management icon verified.");


        // Page heading
        var heading = Page.Locator(PageHeading);

        await heading.WaitForAsync();

        Assert.AreEqual(
            "Customer Group List",
            (await heading.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Customer Group List heading verified.");


        // Settings breadcrumb
        var settingsBreadcrumb =
            Page.Locator(SettingsBreadcrumb);

        Assert.IsTrue(
            await settingsBreadcrumb.IsVisibleAsync(),
            "Settings breadcrumb is not visible.");

        Assert.AreEqual(
            "Settings",
            (await settingsBreadcrumb.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Settings breadcrumb verified.");
    }


    // =====================================================
    // Page Controls
    // =====================================================

    public async Task VerifyCustomerGroupListControlsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CUSTOMER GROUP LIST CONTROLS ==========");


        // Search field
        Assert.IsTrue(
            await Page.Locator(SearchInput).IsVisibleAsync(),
            "Search field is not visible.");

        Console.WriteLine(
            "✔ Search field verified.");


        // Search button
        Assert.IsTrue(
            await Page.Locator(SearchButton).IsVisibleAsync(),
            "Search button is not visible.");

        Console.WriteLine(
            "✔ Search button verified.");


        // Export button
        Assert.IsTrue(
            await Page.Locator(ExportButton).IsVisibleAsync(),
            "Export button is not visible.");

        Console.WriteLine(
            "✔ Export button verified.");


        // Add Customer Group button
        Assert.IsTrue(
            await Page.Locator(AddCustomerGroupButton).IsVisibleAsync(),
            "Add Customer Group button is not visible.");

        Console.WriteLine(
            "✔ Add Customer Group button verified.");


        // Show Entries dropdown
        var showEntries =
            Page.Locator(ShowEntriesDropdown);

        Assert.IsTrue(
            await showEntries.IsVisibleAsync(),
            "Show Entries dropdown is not visible.");

        var options =
            showEntries.Locator("option");

        var optionCount =
            await options.CountAsync();

        Assert.IsTrue(
            optionCount > 0,
            "Show Entries dropdown contains no options.");

        Console.WriteLine(
            $"✔ Show Entries dropdown contains {optionCount} options.");


        // Select 100 entries
        await showEntries.SelectOptionAsync(
            new SelectOptionValue { Label = "100" });

        Console.WriteLine(
            "✔ Show Entries set to 100.");


        // Filter
        var filter =
            Page.Locator(FilterContainer);

        Assert.IsTrue(
            await filter.IsVisibleAsync(),
            "Filter is not visible.");

        Assert.IsTrue(
            (await filter.InnerTextAsync()).Contains("Filter:"),
            "Filter text was not found.");

        Console.WriteLine(
            "✔ Filter verified.");


        // DataTable search
        var tableSearch =
            Page.Locator(TableSearchInput);

        Assert.IsTrue(
            await tableSearch.IsVisibleAsync(),
            "DataTable search field is not visible.");

        await tableSearch.FillAsync("Automation");

        Console.WriteLine(
            "✔ DataTable search field verified.");
        Console.WriteLine(
            "✔ DataTable search value entered: Automation");
    }


    // =====================================================
    // Column Verification
    // =====================================================

    public async Task VerifyCustomerGroupColumnsAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CUSTOMER GROUP COLUMNS ==========");

        string[] expectedColumns =
        {
            "Merchant Group",
            "Code",
            "Description",
            "Customer Parent Group",
            "Account Prefix",
            "Action"
        };

        var headers =
            Page.Locator($"{DataTable} thead tr th");

        var headerCount =
            await headers.CountAsync();

        Assert.AreEqual(
            expectedColumns.Length,
            headerCount,
            $"Expected {expectedColumns.Length} columns but found {headerCount}.");

        Console.WriteLine(
            $"✔ Header cells found: {headerCount}");

        for (int i = 0; i < expectedColumns.Length; i++)
        {
            var actualHeader =
                (await headers.Nth(i).InnerTextAsync()).Trim();

            Assert.AreEqual(
                expectedColumns[i],
                actualHeader,
                $"Column {i + 1} mismatch.");

            Console.WriteLine(
                $"✔ {actualHeader}");
        }
    }


    // =====================================================
    // Data Type Verification
    // =====================================================

    public async Task VerifyCustomerGroupDataTypesAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CUSTOMER GROUP DATA TYPES ==========");

        var dataRows =
            Page.Locator($"{DataTable} tbody tr");

        var rowCount =
            await dataRows.CountAsync();

        Assert.IsTrue(
            rowCount > 0,
            "No Customer Group records are available to verify data types.");

        Console.WriteLine(
            $"✔ Customer Group data rows found: {rowCount}");

        var firstRow =
            dataRows.First;

        var cells =
            firstRow.Locator("td");


        // Merchant Group
        var merchantGroup =
            (await cells.Nth(0).InnerTextAsync()).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(merchantGroup),
            "Merchant Group value is empty.");

        Console.WriteLine(
            "✔ Merchant Group data type: String");


        // Code
        var code =
            (await cells.Nth(1).InnerTextAsync()).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(code),
            "Code value is empty.");

        Console.WriteLine(
            "✔ Code data type: String");


        // Description
        var description =
            (await cells.Nth(2).InnerTextAsync()).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(description),
            "Description value is empty.");

        Console.WriteLine(
            "✔ Description data type: String");


        // Customer Parent Group
        var customerParentGroup =
            (await cells.Nth(3).InnerTextAsync()).Trim();

        if (string.IsNullOrWhiteSpace(customerParentGroup))
        {
            Console.WriteLine(
                "⚠ Customer Parent Group has no available data.");
        }
        else
        {
            Console.WriteLine(
                "✔ Customer Parent Group data type: String");
        }


        // Account Prefix
        var accountPrefix =
            (await cells.Nth(4).InnerTextAsync()).Trim();

        if (string.IsNullOrWhiteSpace(accountPrefix))
        {
            Console.WriteLine(
                "⚠ Account Prefix has no available data.");
        }
        else
        {
            Console.WriteLine(
                "✔ Account Prefix data type: String");
        }
    }


    // =====================================================
    // Group Details Action
    // =====================================================

    public async Task VerifyGroupDetailsActionAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GROUP DETAILS ACTION ==========");

        var groupDetails =
            Page.Locator(GroupDetailsAction).First;

        Assert.IsTrue(
            await groupDetails.IsVisibleAsync(),
            "Group Details action is not visible.");

        var tagName =
            await groupDetails.EvaluateAsync<string>(
                "element => element.tagName");

        Assert.AreEqual(
            "A",
            tagName.ToUpperInvariant(),
            "Group Details action is not a link.");

        Console.WriteLine(
            "✔ Group Details action verified as a link.");
    }
}