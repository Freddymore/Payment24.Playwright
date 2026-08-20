using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using System.Text.RegularExpressions;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class CustomerMonthlyInvoicePage : BasePage
{
    // =====================================================
    // Page Locators
    // =====================================================

    private const string DashboardIcon =
        "a[href='DashboardNew.aspx']";

    private const string PageHeading =
        "#page-title";

    private const string CustomerInvoiceBreadcrumb =
        "a[href='ReportFuelDelivery.aspx']";

    // =====================================================
    // Date Controls
    // =====================================================

    private const string FromDate =
        "#cphBody_txtFromDate";

    private const string ToDate =
        "#cphBody_txtToDate";

    private const string CalendarPopup =
        "#cphBody_calActivationDate_popupDiv";

    private const string CalendarHeader =
        "#cphBody_calActivationDate_header";

    private const string CalendarTitle =
        "#cphBody_calActivationDate_title";

    private const string CalendarPreviousArrow =
        "#cphBody_calActivationDate_prevArrow";

    private const string CalendarBody =
        "#cphBody_calActivationDate_body";

    private const string CalendarYears =
        "#cphBody_calActivationDate_yearsBody";

    private const string CalendarYear =
        "#cphBody_calActivationDate_year_1_0";

    private const string CalendarMonths =
        "#cphBody_calActivationDate_monthsBody";

    private const string CalendarMonth =
        "#cphBody_calActivationDate_month_0_1";

    private const string CalendarDays =
        "#cphBody_calActivationDate_days";

    private const string CalendarDay =
        "#cphBody_calActivationDate_day_1_0";

    // =====================================================
    // Invoice Controls
    // =====================================================

   /* private const string FleetDropdown =
        "#cphBody_selFleet";*/

    private const string GenerateInvoiceButton =
    "#cphBody_btnGetFleetCustomerStatement";

    // =====================================================
    // Constructor
    // =====================================================

    public CustomerMonthlyInvoicePage(IPage page)
        : base(page)
    {
    }

    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await Page.GotoAsync(
            "https://admin-stage.payment24.co/CustomerMonthlyInvoice.aspx");

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);
    }

    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyCustomerMonthlyInvoicePageAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=====================================");
        Console.WriteLine("VERIFYING CUSTOMER MONTHLY INVOICE PAGE");
        Console.WriteLine("=====================================");

        // Dashboard icon
        Assert.IsTrue(
            await Page.Locator(DashboardIcon).IsVisibleAsync(),
            "Dashboard icon is not visible.");

        Console.WriteLine(
            "✔ Dashboard icon verified.");

        // Page heading
        var heading =
            Page.Locator(PageHeading);

        await heading.WaitForAsync();

        Assert.AreEqual(
            "Customer Invoice",
            (await heading.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Customer Invoice heading verified.");

        // Customer Invoice breadcrumb
        var breadcrumb =
            Page.Locator(CustomerInvoiceBreadcrumb);

        Assert.AreEqual(
            "Customer Invoice",
            (await breadcrumb.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Customer Invoice breadcrumb verified.");
    }

    // =====================================================
    // From Date Verification
    // =====================================================

    public async Task VerifyFromDateCalendarAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== FROM DATE CALENDAR ==========");

        // =====================================================
        // Open From Date calendar
        // =====================================================

        var fromDate =
            Page.Locator(FromDate);

        Assert.IsTrue(
            await fromDate.IsVisibleAsync(),
            "From Date field is not visible.");

        await fromDate.ClickAsync();

        Console.WriteLine(
            "✔ From Date field clicked.");

        // =====================================================
        // Verify calendar popup
        // =====================================================

        var calendarPopup =
            Page.Locator(CalendarPopup);

        await calendarPopup.WaitForAsync();

        Assert.IsTrue(
            await calendarPopup.IsVisibleAsync(),
            "From Date calendar is not visible.");

        Console.WriteLine(
            "✔ From Date calendar displayed.");

        // =====================================================
        // Calendar header
        // =====================================================

        var calendarHeader =
            Page.Locator(CalendarHeader);

        Assert.IsTrue(
            await calendarHeader.IsVisibleAsync(),
            "Calendar header is not visible.");

        Console.WriteLine(
            "✔ Calendar header verified.");

        // =====================================================
        // Click calendar title
        // =====================================================

        var calendarTitle =
            Page.Locator(CalendarTitle);

        await calendarTitle.ClickAsync();

        Console.WriteLine(
            "✔ Calendar title clicked.");

        // =====================================================
        // Previous month
        // =====================================================

        var previousArrow =
            Page.Locator(CalendarPreviousArrow);

        Assert.IsTrue(
            await previousArrow.IsVisibleAsync(),
            "Previous calendar arrow is not visible.");

        await previousArrow.ClickAsync();

        Console.WriteLine(
            "✔ Previous month arrow clicked.");

        // =====================================================
        // Inspect calendar state
        // =====================================================

        Console.WriteLine();
        Console.WriteLine(
            "========== CALENDAR VIEW STATE ==========");

        var yearsBody =
            Page.Locator(CalendarYears);

        var monthsBody =
            Page.Locator(CalendarMonths);

        var daysBody =
            Page.Locator(CalendarDays);

        Console.WriteLine(
            $"Years view visible: {await yearsBody.IsVisibleAsync()}");

        Console.WriteLine(
            $"Months view visible: {await monthsBody.IsVisibleAsync()}");

        Console.WriteLine(
            $"Days view visible: {await daysBody.IsVisibleAsync()}");

        // =====================================================
        // FIND ALL MONTHS
        // =====================================================

        Console.WriteLine();
        Console.WriteLine(
            "========== AVAILABLE MONTHS ==========");

        var months =
            monthsBody.Locator("[id*='month_']");

        var monthCount =
            await months.CountAsync();

        Console.WriteLine(
            $"Month elements found: {monthCount}");

        for (int i = 0; i < monthCount; i++)
        {
            var month =
                months.Nth(i);

            var monthId =
                await month.GetAttributeAsync("id");

            var monthText =
                (await month.InnerTextAsync()).Trim();

            var monthClass =
                await month.GetAttributeAsync("class");

            Console.WriteLine(
                $"Month [{i}] | ID: {monthId} | Text: '{monthText}' | Class: '{monthClass}'");
        }

        // =====================================================
        // SELECT MONTH
        // =====================================================

        // For now, select the first available month.
        // We will make this date-specific once we confirm
        // the calendar structure.

        Assert.IsTrue(
            monthCount > 0,
            "No month elements were found in the calendar.");

        var selectedMonth =
            months.First;

        Console.WriteLine();
        Console.WriteLine(
            $"Selecting month: {(await selectedMonth.InnerTextAsync()).Trim()}");

        await selectedMonth.ClickAsync();

        Console.WriteLine(
            "✔ Calendar month selected.");

        // =====================================================
        // VERIFY MONTH VIEW CLOSED
        // =====================================================

        await Page.WaitForTimeoutAsync(300);

        Console.WriteLine();
        Console.WriteLine(
            "========== AFTER MONTH SELECTION ==========");

        Console.WriteLine(
            $"Months view visible: {await monthsBody.IsVisibleAsync()}");

        Console.WriteLine(
            $"Days view visible: {await daysBody.IsVisibleAsync()}");

        // =====================================================
        // DAY SELECTION
        // =====================================================

        Assert.IsTrue(
            await daysBody.IsVisibleAsync(),
            "Calendar day selection did not become visible after selecting month.");

        Console.WriteLine(
            "✔ Calendar day selection displayed.");

        // =====================================================
        // FIND ALL DAYS
        // =====================================================

        var dayElements =
            daysBody.Locator("[id*='day_']");

        var dayCount =
            await dayElements.CountAsync();

        Console.WriteLine(
            $"Day elements found: {dayCount}");

        for (int i = 0; i < Math.Min(dayCount, 10); i++)
        {
            var day =
                dayElements.Nth(i);

            Console.WriteLine(
                $"Day [{i}] | ID: {await day.GetAttributeAsync("id")} | Text: '{(await day.InnerTextAsync()).Trim()}' | Class: '{await day.GetAttributeAsync("class")}'");
        }

        // =====================================================
        // SELECT DAY
        // =====================================================

        Assert.IsTrue(
            dayCount > 0,
            "No day elements were found in the calendar.");

        var selectedDay =
            dayElements.First;

        Console.WriteLine();
        Console.WriteLine(
            $"Selecting day: {(await selectedDay.InnerTextAsync()).Trim()}");

        await selectedDay.ClickAsync();

        Console.WriteLine(
            "✔ Calendar day selected.");

        // =====================================================
        // VERIFY CALENDAR CLOSED
        // =====================================================

        await Page.WaitForTimeoutAsync(300);

        Assert.IsFalse(
            await calendarPopup.IsVisibleAsync(),
            "Calendar is still visible after selecting the day.");

        Console.WriteLine(
            "✔ From Date calendar closed.");

        // =====================================================
        // VERIFY FROM DATE VALUE
        // =====================================================

        var selectedDate =
            await fromDate.InputValueAsync();

        Console.WriteLine(
            $"From Date value: {selectedDate}");

        Assert.IsFalse(
            string.IsNullOrWhiteSpace(selectedDate),
            "From Date was not populated after selecting the date.");

        Console.WriteLine(
            "✔ From Date populated successfully.");

        Console.WriteLine(
            "✔ From Date calendar selection completed.");
    }

    // =====================================================
    // YEAR SELECTION
    // =====================================================


    /*var yearsBody =
        Page.Locator(CalendarYears);

    await yearsBody.WaitForAsync();

    Assert.IsTrue(
        await yearsBody.IsVisibleAsync(),
        "Calendar year selection is not visible.");

    Console.WriteLine(
        "✔ Calendar year selection displayed.");

    // Select year
    var year =
        Page.Locator(CalendarYear);

    await year.WaitForAsync();

    Assert.IsTrue(
        await year.IsVisibleAsync(),
        "Calendar year is not visible.");

    await year.ClickAsync();

    Console.WriteLine(
        "✔ Calendar year selected.");

    // =====================================================
    // MONTH SELECTION
    // =====================================================

    var monthsBody =
        Page.Locator(CalendarMonths);

    await monthsBody.WaitForAsync();

    Assert.IsTrue(
        await monthsBody.IsVisibleAsync(),
        "Calendar month selection is not visible.");

    Console.WriteLine(
        "✔ Calendar month selection displayed.");

    var month =
        Page.Locator(CalendarMonth);

    await month.WaitForAsync();

    Assert.IsTrue(
        await month.IsVisibleAsync(),
        "Calendar month is not visible.");

    await month.ClickAsync();

    Console.WriteLine(
        "✔ Calendar month selected.");

    // =====================================================
    // DAY SELECTION
    // =====================================================

    var days =
        Page.Locator(CalendarDays);

    await days.WaitForAsync();

    Assert.IsTrue(
        await days.IsVisibleAsync(),
        "Calendar day selection is not visible.");

    Console.WriteLine(
        "✔ Calendar day selection displayed.");

    var day =
        Page.Locator(CalendarDay);

    await day.WaitForAsync();

    Assert.IsTrue(
        await day.IsVisibleAsync(),
        "Calendar day is not visible.");

    await day.ClickAsync();

    Console.WriteLine(
        "✔ Calendar day selected.");

    Console.WriteLine(
        "✔ From Date calendar selection completed.");*/
    // }

    // =====================================================
    // DIAGNOSTIC - CHECK CALENDAR VIEWS
    // =====================================================

  /*  Console.WriteLine();
        Console.WriteLine("========== CALENDAR VIEW STATE ==========");

        var yearsBody =
            Page.Locator(CalendarYears);

        var monthsBody =
            Page.Locator(CalendarMonths);

        var daysBody =
            Page.Locator(CalendarDays);

        Console.WriteLine(
        $"Years view visible: {await yearsBody.IsVisibleAsync()}");

        Console.WriteLine(
            $"Months view visible: {await monthsBody.IsVisibleAsync()}");

        Console.WriteLine(
            $"Days view visible: {await daysBody.IsVisibleAsync()}");
    }*/
    public async Task VerifyToDateDisabledAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== TO DATE FIELD ==========");

        var toDate = Page.Locator(ToDate);

        Assert.IsTrue(
            await toDate.IsVisibleAsync(),
            "To Date field is not visible.");

        var classAttribute =
            await toDate.GetAttributeAsync("class") ?? "";

        Assert.IsTrue(
            classAttribute.Contains("aspNetDisabled"),
            $"To Date field is not disabled. Class: {classAttribute}");

        Console.WriteLine(
            "✔ To Date field is disabled.");
    }
    // =====================================================
    // Fleet Dropdown
    // =====================================================

    /*public async Task VerifyFleetDropdownAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== FLEET DROPDOWN ==========");

        var fleetDropdown =
            Page.Locator(FleetDropdown);

        Assert.IsTrue(
            await fleetDropdown.IsVisibleAsync(),
            "Fleet dropdown is not visible.");

        var optionCount =
            await fleetDropdown.Locator("option").CountAsync();

        Assert.IsTrue(
            optionCount > 0,
            "Fleet dropdown contains no options.");

        Console.WriteLine(
            $"✔ Fleet dropdown verified. Options: {optionCount}");
    }*/


    // =====================================================
    // Select Fleet
    // =====================================================

    /*public async Task SelectFleetAsync(string fleetValue)
    {
        var fleetDropdown =
            Page.Locator(FleetDropdown);

        await fleetDropdown.SelectOptionAsync(fleetValue);

        Console.WriteLine(
            $"✔ Fleet selected: {fleetValue}");
    }*/

    // =====================================================
    // Generate Invoice
    // =====================================================

    public async Task GenerateInvoiceAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== GENERATE CUSTOMER INVOICE ==========");

        var generateButton =
            Page.Locator(GenerateInvoiceButton);

        // =====================================================
        // Verify button exists
        // =====================================================

        var count =
            await generateButton.CountAsync();

        Console.WriteLine(
            $"Generate Invoice locator count: {count}");

        Assert.IsTrue(
            count > 0,
            "Generate Invoice button was not found.");

        // =====================================================
        // Verify visible
        // =====================================================

        Assert.IsTrue(
            await generateButton.IsVisibleAsync(),
            "Generate Invoice button is not visible.");

        Console.WriteLine(
            "✔ Generate Invoice button is visible.");

        // =====================================================
        // Verify enabled
        // =====================================================

        Assert.IsTrue(
            await generateButton.IsEnabledAsync(),
            "Generate Invoice button is disabled.");

        Console.WriteLine(
            "✔ Generate Invoice button is enabled.");

        // =====================================================
        // Click
        // =====================================================

        await generateButton.ClickAsync();

        Console.WriteLine(
            "✔ Generate Invoice button clicked.");

        // =================================================
        // Wait for response/page processing
        // =================================================

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);

        // =================================================
        // Detect known application error
        // =================================================

        var errorMessage =
            Page.GetByText(
                "Something went wrong!",
                new PageGetByTextOptions
                {
                    Exact = true
                });

        if (await errorMessage.IsVisibleAsync())
        {
            var bodyText =
                await Page.Locator("body")
                    .InnerTextAsync();

            var referenceMatch =
                Regex.Match(
                    bodyText,
                    @"Reference number:\s*(\d+)",
                    RegexOptions.IgnoreCase);

            var referenceNumber =
                referenceMatch.Success
                    ? referenceMatch.Groups[1].Value
                    : "Not available";

            Console.WriteLine();

            Console.WriteLine(
                "=====================================");

            Console.WriteLine(
                "❌ CUSTOMER INVOICE GENERATION FAILED");

            Console.WriteLine(
                "=====================================");

            Console.WriteLine(
                "Error: Something went wrong!");

            Console.WriteLine(
                "Support: bluefuelsupport@standardbank.com.na");

            Console.WriteLine(
                $"Reference Number: {referenceNumber}");

            Console.WriteLine(
                "=====================================");

            Assert.Fail(
                $"Generate Invoice returned an application error. " +
                $"Reference number: {referenceNumber}");
        }

        // =================================================
        // No application error
        // =================================================

        Console.WriteLine(
            "✔ No application error detected.");

        Console.WriteLine(
            "✔ Customer invoice generation completed.");
    }
}

