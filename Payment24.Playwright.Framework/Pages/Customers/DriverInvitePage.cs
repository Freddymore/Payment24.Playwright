using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class DriverInvitePage : BasePage
{
    private const string PageUrl =
        "https://admin-stage.payment24.co/DriverInvite.aspx";

    // Main page
    private const string CustomerManagementIcon =
        "img[src*='customer_management_icon']";

    private const string PageHeading =
        "h2.heading";

    private const string Breadcrumb =
        "a.breadcrumb.breadbtn2";

    // Search controls
    private const string FleetDropdown =
        "#cphBody_selFleet";

    private const string StatusSearch =
        "#cphBody_txtSearch";

    private const string SearchButton =
        "#cphBody_btnSearch";

    // Grid

    private const string GridFilter =
        "input[type='search']";

    private const string CustomerGrid =
        "#cphBody_grvDriverList";

    private const string GridHeaders =
        "#cphBody_grvDriverList thead tr th";

    private const string GridRows =
        "#cphBody_grvDriverList tbody tr";

    

    // Invite action
    private const string SendInviteCodeLink =
        "a[data-original-title='Send Invite Code']";

    public DriverInvitePage(IPage page)
        : base(page)
    {
    }

    public async Task NavigateAsync()
    {
        await Page.GotoAsync(PageUrl);

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);

        await Page.WaitForTimeoutAsync(1000);
    }

    public async Task VerifyDriverInvitePageAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "==============================================");
        Console.WriteLine(
            "VERIFYING DRIVER INVITE PAGE");
        Console.WriteLine(
            "==============================================");

        await VerifyPageLoadedAsync();
        await VerifyMainPageDetailsAsync();
        await ConfigureSearchAsync();
        await VerifyGridAsync();
        await VerifyColumnDataTypesAsync();
        await VerifyManagerColumnAsync();
        await VerifySendInviteLinkAsync();
        await SendInviteCodeAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Driver Invite page verified successfully.");
    }

    private async Task VerifyPageLoadedAsync()
    {
        await Page.Locator(PageHeading).WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        var heading = await Page
            .Locator(PageHeading)
            .InnerTextAsync();

        Assert.AreEqual(
            "Send Driver Invite Codes",
            heading);

        Console.WriteLine(
            "✔ Driver Invite page loaded.");
    }

    private async Task VerifyMainPageDetailsAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "========== MAIN PAGE DETAILS ==========");

        var customerManagementIcon =
            Page.Locator(CustomerManagementIcon);

        Assert.IsTrue(
            await customerManagementIcon.IsVisibleAsync(),
            "Customer Management icon is not visible.");

        Console.WriteLine(
            "✔ Customer Management icon verified.");

        var heading =
            Page.Locator(PageHeading);

        Assert.AreEqual(
            "Send Driver Invite Codes",
            await heading.InnerTextAsync());

        Console.WriteLine(
            "✔ Send Driver Invite Codes heading verified.");

        var breadcrumb =
            Page.Locator(Breadcrumb);

        Assert.AreEqual(
            "Send Driver Invite Codes",
            await breadcrumb.InnerTextAsync());

        Console.WriteLine(
            "✔ Send Driver Invite Codes breadcrumb verified.");
    }

    private async Task ConfigureSearchAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "========== SEARCH CONTROLS ==========");

        var fleetDropdown =
            Page.Locator(FleetDropdown);

        await fleetDropdown.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        await fleetDropdown.SelectOptionAsync("75765");

        Console.WriteLine(
            "✔ Fleet selected: 75765 DO NOT DELETE(BP HO)");

        var statusField =
            Page.Locator(StatusSearch);

        await statusField.FillAsync("Active");

        Console.WriteLine(
            "✔ Status search value entered: Active.");

        var searchButton =
            Page.Locator(SearchButton);

        Assert.IsTrue(
            await searchButton.IsVisibleAsync(),
            "Search button is not visible.");

        Console.WriteLine(
            "✔ Search button verified.");

        await searchButton.ClickAsync();

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);

        await Page.WaitForTimeoutAsync(2000);

        Console.WriteLine(
            "✔ Driver search completed.");

        var filter =
            Page.Locator(GridFilter);

        await filter.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        await filter.FillAsync("QA Payment");

        Console.WriteLine(
            "✔ Grid filter value entered: QA Payment");
    }

    private async Task VerifyGridAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "========== GRID ==========");

        var grid =
            Page.Locator(CustomerGrid);

        Assert.IsTrue(
            await grid.IsVisibleAsync(),
            "Driver Invite grid is not visible.");

        Console.WriteLine(
            "✔ Driver Invite grid verified.");

        var rows =
            Page.Locator(GridRows);

        Assert.IsTrue(
            await rows.CountAsync() > 0,
            "No driver invite records found.");

        Console.WriteLine(
            $"✔ Driver Invite grid contains {await rows.CountAsync()} row(s).");
    }

    private async Task VerifyColumnDataTypesAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "========== GRID HEADERS ==========");

        string[] expectedHeaders =
        {
            "Department",
            "Status",
            "Card Number",
            "Name",
            "Type",
            "Manager",
            "Mobile Number",
            "Email",
            " "
        };

        var headers =
            Page.Locator(GridHeaders);

        var headerCount =
            await headers.CountAsync();

        Assert.AreEqual(
            expectedHeaders.Length,
            headerCount,
            "Unexpected number of grid headers.");

        for (int i = 0; i < expectedHeaders.Length; i++)
        {
            var actual =
                (await headers.Nth(i).InnerTextAsync()).Trim();

            var expected =
                expectedHeaders[i].Trim();

            Assert.AreEqual(
                expected,
                actual);

            Console.WriteLine(
                $"✔ Header {i + 1}: {expectedHeaders[i]}");
        }

        Console.WriteLine();
        Console.WriteLine(
            "========== GRID DATA TYPES ==========");

        await VerifyColumnStringAsync(
            1, "Department");

        await VerifyColumnStringAsync(
            2, "Status");

        await VerifyColumnStringAsync(
            3, "Card Number");

        await VerifyColumnStringAsync(
            4, "Name");

        await VerifyColumnStringAsync(
            5, "Type");

        await VerifyColumnStringAsync(
            7, "Mobile Number");

        await VerifyColumnStringAsync(
            8, "Email");
    }

    private async Task VerifyColumnStringAsync(
        int columnNumber,
        string columnName)
    {
        var cell =
            Page.Locator(
                $"{GridRows} td:nth-child({columnNumber})")
            .First;

        await cell.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        var value =
            (await cell.InnerTextAsync()).Trim();

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(value),
            $"{columnName} column contains no value.");

        Console.WriteLine(
            $"✔ {columnName} data type is a string. Value: {value}");
    }

    private async Task VerifyManagerColumnAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "========== MANAGER COLUMN ==========");

        var managerCheckbox =
            Page.Locator(
                $"{GridRows} input[disabled='disabled']")
            .First;

        Assert.IsTrue(
            await managerCheckbox.CountAsync() > 0,
            "Manager checkbox was not found.");

        Assert.IsTrue(
            await managerCheckbox.IsDisabledAsync(),
            "Manager checkbox is not disabled.");

        Console.WriteLine(
            "✔ Manager column checkbox is displayed and disabled.");
    }

    private async Task VerifySendInviteLinkAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "========== CUSTOMER ACTION LINKS ==========");

        var sendInviteLink =
            Page.Locator(SendInviteCodeLink).First;

        await sendInviteLink.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        Assert.AreEqual(
            "a",
            await sendInviteLink.EvaluateAsync<string>(
                "element => element.tagName.toLowerCase()"));

        var title =
            await sendInviteLink.GetAttributeAsync(
                "data-original-title");

        Assert.AreEqual(
            "Send Invite Code",
            title);

        Console.WriteLine(
            "✔ Send Invite Code button icon is a link.");
    }

    private async Task SendInviteCodeAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "========== SEND INVITE CODE ==========");

        var sendInviteLink =
            Page.Locator(SendInviteCodeLink).First;

        await sendInviteLink.ClickAsync();

        await Page.WaitForTimeoutAsync(2000);

        Console.WriteLine(
            "✔ Send Invite Code button clicked.");
        Console.WriteLine(
            "✔ Invite code action executed.");
    }
}