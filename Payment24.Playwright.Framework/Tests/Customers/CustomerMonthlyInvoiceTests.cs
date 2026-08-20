using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class CustomerMonthlyInvoiceTests : BaseTest
{
    [TestMethod]
    public async Task Customer_Monthly_Invoice_Page_Loads()
    {
        await StartPortalSessionAsync("MPAY");

        var customerInvoicePage =
            new CustomerMonthlyInvoicePage(Page);

        // =====================================================
        // Navigate
        // =====================================================

        await customerInvoicePage.NavigateAsync();

        // =====================================================
        // Verify Page
        // =====================================================

        await customerInvoicePage
            .VerifyCustomerMonthlyInvoicePageAsync();

        // =====================================================
        // Verify From Date Calendar
        // =====================================================

        await customerInvoicePage
     .VerifyFromDateCalendarAsync();

        // =====================================================
        // Verify To Date
        // =====================================================

        await customerInvoicePage
            .VerifyToDateDisabledAsync();

        // =====================================================
        // Verify Fleet Dropdown
        // =====================================================

       /*await customerInvoicePage
            .VerifyFleetDropdownAsync();*/

        // =====================================================
        // Select Fleet
        // =====================================================

       /* await customerInvoicePage
            .SelectFleetAsync("74828");*/

        // =====================================================
        // Generate Invoice
        // =====================================================

        await customerInvoicePage
            .GenerateInvoiceAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Customer Monthly Invoice page test completed successfully.");
    }
}