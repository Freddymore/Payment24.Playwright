using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class CustomerDiscountBulkUploadTests : BaseTest
{
    [TestMethod]
    public async Task Customer_Discount_Bulk_Upload_Page_Loads()
    {
        await StartPortalSessionAsync("IMPL");

        var customerDiscountPage =
            new CustomerDiscountBulkUploadPage(Page);

        await customerDiscountPage.NavigateAsync();

        await customerDiscountPage.VerifyCustomerDiscountBulkUploadPageAsync();

        await customerDiscountPage.VerifyUploadWithoutFileAsync();

        await customerDiscountPage.VerifyExcelUploadHelpAsync();


        // ==========================================
        // TEST DATA FILE PATHS
        // ==========================================

        var invalidFilePath = Path.Combine(
            AppContext.BaseDirectory,
            "TestData",
            "CustomerDiscount",
            "TestingImage.png");

        var excelFilePath = Path.Combine(
            AppContext.BaseDirectory,
            "TestData",
            "CustomerDiscount",
            "CustomerDiscoutUploads.xlsx");


        // ==========================================
        // INVALID FILE TYPE TEST
        // ==========================================

        await customerDiscountPage.VerifyInvalidFileUploadAsync(
            invalidFilePath);


        // ==========================================
        // INVALID CUSTOMER ACCOUNT TEST
        // ==========================================

        await customerDiscountPage.VerifyInvalidCustomerAccountUploadAsync(
            excelFilePath);


        Console.WriteLine();
        Console.WriteLine(
            "✔ Customer Discount Bulk Upload page smoke test completed successfully.");
    }
}