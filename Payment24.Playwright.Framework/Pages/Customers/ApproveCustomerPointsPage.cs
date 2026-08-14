using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class ApproveCustomerPointsPage : BasePage
{
    // =====================================================
    // Page Locators
    // =====================================================

    private const string PageHeading =
        "h2.heading";

    private const string ShowEntriesDropdown =
        "select[name='DataTables_Table_0_length']";

    private const string SearchInput =
        "input[type='search']";

    private const string GridHeaders =
        "table thead tr th";


    // =====================================================
    // Constructor
    // =====================================================

    public ApproveCustomerPointsPage(IPage page)
        : base(page)
    {
    }


    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await Page.GotoAsync(
            "https://admin-stage.payment24.co/ApproveCustomerPoints");

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);
    }


    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyApproveCustomerPointsPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=====================================");
        Console.WriteLine("VERIFYING APPROVE CUSTOMER POINTS PAGE");
        Console.WriteLine("=====================================");


        // Verify page heading
        var heading = Page.Locator(PageHeading);

        await heading.WaitForAsync();

        Assert.AreEqual(
            "Approve Customer Points",
            (await heading.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Approve Customer Points heading verified.");


        // Verify Show Entries dropdown
        var showEntriesDropdown =
            Page.Locator(ShowEntriesDropdown);

        Assert.IsTrue(
            await showEntriesDropdown.IsVisibleAsync(),
            "Show Entries dropdown is not visible.");

        var options =
            showEntriesDropdown.Locator("option");

        var optionsCount =
            await options.CountAsync();

        Assert.AreEqual(
            4,
            optionsCount,
            "Show Entries dropdown should have 4 options.");

        Console.WriteLine(
            $"✔ Show Entries dropdown contains {optionsCount} options.");


        // Verify column headers
        string[] expectedColumns =
        {
            "Reference",
            "Customer",
            "Base Points",
            "Bonus Points",
            "Date Requested",
            "Date Modified",
            "Approver",
            "Requester",
            "Transaction Method",
            "Status",
            "Action"
        };

        var headers =
            Page.Locator(GridHeaders);

        var headerCount =
            await headers.CountAsync();

        Console.WriteLine(
            $"✔ Header cells found: {headerCount}");

        Assert.AreEqual(
            11,
            headerCount,
            "Column count mismatch.");


        for (int i = 0; i < expectedColumns.Length; i++)
        {
            var actualHeader =
                (await headers.Nth(i).InnerTextAsync()).Trim();

            Assert.AreEqual(
                expectedColumns[i],
                actualHeader,
                $"Column {i + 1} does not match.");

            Console.WriteLine(
                $"✔ {actualHeader}");
        }


        // Verify search input
        var searchInput =
            Page.Locator(SearchInput);

        Assert.IsTrue(
            await searchInput.IsVisibleAsync(),
            "Search input is not displayed.");

        Console.WriteLine(
            "✔ Search input verified.");


        Console.WriteLine();
        Console.WriteLine(
            "✔ Approve Customer Points page verified.");
    }
}