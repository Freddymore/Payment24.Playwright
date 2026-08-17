using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class CustomerDiscountBulkUploadPage : BasePage
{
    // =====================================================
    // Page Locators
    // =====================================================

    private const string DashboardIcon =
        "img.icon";

    private const string PageHeading =
        "h2.heading";

    private const string HomeIcon =
        "i.ti-home";

    private const string Breadcrumb =
        "a.breadcrumb.breadbtn2";

    private const string FileLabel =
        "label.control-label";

    private const string FileUpload =
        "#cphBody_FileUploadExcel";

    private const string UploadButton =
        "#cphBody_btnUpload";

    private const string HelpButton =
        "a[onclick='toggleExcelFormat()']";

    private const string SelectFileLabel =
        "label.col-lg-3.control-label";

    private const string ErrorLog =
        "#cphBody_lblErrorLog";

    private const string Message =
        "#cphBody_lblMessage";


    // =====================================================
    // Constructor
    // =====================================================

    public CustomerDiscountBulkUploadPage(IPage page)
        : base(page)
    {
    }


    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await Page.GotoAsync(
            "https://admin-stage.payment24.co/CustomerDiscountBulkUpload.aspx");

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);
    }


    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyCustomerDiscountBulkUploadPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=====================================");
        Console.WriteLine("VERIFYING CUSTOMER DISCOUNT UPLOAD PAGE");
        Console.WriteLine("=====================================");


        // Dashboard icon
        Assert.IsTrue(
            await Page.Locator(DashboardIcon).IsVisibleAsync(),
            "Dashboard icon is not visible.");

        Console.WriteLine("✔ Dashboard icon verified.");


        // Page heading
        var heading = Page.Locator(PageHeading);

        await heading.WaitForAsync();

        Assert.AreEqual(
            "Customer Discount Uploads",
            (await heading.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Customer Discount Uploads heading verified.");


        // Home icon
        Assert.IsTrue(
            await Page.Locator(HomeIcon).IsVisibleAsync(),
            "Home icon is not visible.");

        Console.WriteLine("✔ Home icon verified.");


        // Breadcrumb
        var breadcrumb = Page.Locator(Breadcrumb);

        Assert.AreEqual(
            "Customer Discount Uploads",
            (await breadcrumb.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Customer Discount Uploads breadcrumb verified.");


        // File label
        var fileLabel = Page.Locator(FileLabel).First;

        Assert.AreEqual(
            "File",
            (await fileLabel.InnerTextAsync()).Trim());

        Console.WriteLine("✔ File label verified.");


        // File upload control
        Assert.IsTrue(
            await Page.Locator(FileUpload).IsVisibleAsync(),
            "File upload control is not visible.");

        Console.WriteLine("✔ File upload control verified.");


        // Upload button
        Assert.IsTrue(
            await Page.Locator(UploadButton).IsVisibleAsync(),
            "Upload button is not visible.");

        Console.WriteLine("✔ Upload button verified.");
    }

    public async Task VerifyUploadWithoutFileAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== UPLOAD WITHOUT FILE ==========");

        // Click Upload without selecting a file
        await Page.Locator(UploadButton).ClickAsync();

        Console.WriteLine("✔ Upload button clicked without selecting a file.");

        // Verify prompt message
        var uploadPromptMessage =
            Page.Locator(".form-group > .col-lg-6");

        await uploadPromptMessage.WaitForAsync();

        var message =
            (await uploadPromptMessage.InnerTextAsync()).Trim();

        Assert.AreEqual(
            "No file selected, please select a file.",
            message,
            $"Unexpected upload prompt: {message}");

        Console.WriteLine(
            $"✔ Upload prompt verified: {message}");


    }

    public async Task VerifyExcelUploadHelpAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== EXCEL UPLOAD HELP ==========");

        var helpButton =
            Page.Locator(HelpButton);

        Assert.IsTrue(
            await helpButton.IsVisibleAsync(),
            "Excel upload help button is not visible.");

        await helpButton.ClickAsync();

        Console.WriteLine(
            "✔ Excel upload help button clicked.");

        var selectFileLabel =
            Page.Locator(
                "label.col-lg-3.control-label"
            ).Filter(
                new() { HasText = "Select File to Upload" }
            );

        await selectFileLabel.WaitForAsync();

        Assert.AreEqual(
            "Select File to Upload",
            (await selectFileLabel.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Select File to Upload label verified.");
    }

    public async Task VerifyInvalidFileUploadAsync(string filePath)
    {
        Console.WriteLine();
        Console.WriteLine("========== INVALID FILE UPLOAD ==========");

        // Select invalid file
        await Page.Locator(FileUpload)
            .SetInputFilesAsync(filePath);

        Console.WriteLine(
            $"✔ Invalid file selected: {Path.GetFileName(filePath)}");

        // Click Upload
        await Page.Locator(UploadButton)
            .ClickAsync();

        Console.WriteLine("✔ Upload button clicked.");

        // Verify error message
        var errorPromptMessage =
            Page.Locator(ErrorLog);

        await errorPromptMessage.WaitForAsync();

        var message =
            (await errorPromptMessage.InnerTextAsync()).Trim();

        Assert.AreEqual(
            "Only excel files are allowed .",
            message,
            $"Unexpected error message: {message}");

        Console.WriteLine(
            $"✔ Invalid file error verified: {message}");
    }

    public async Task VerifyInvalidCustomerAccountUploadAsync(string filePath)
    {
        Console.WriteLine();
        Console.WriteLine("========== INVALID CUSTOMER ACCOUNT UPLOAD ==========");

        // Select Excel file
        await Page.Locator(FileUpload)
            .SetInputFilesAsync(filePath);

        Console.WriteLine("✔ Excel file selected.");

        // Click Upload
        await Page.Locator(UploadButton)
            .ClickAsync();

        Console.WriteLine("✔ Upload button clicked.");

        // Verify error message
        var errorMessage =
            Page.Locator(Message);

        await errorMessage.WaitForAsync();

        var message =
            (await errorMessage.InnerTextAsync()).Trim();

        Assert.AreEqual(
            "Customer account number does not exist",
            message,
            $"Unexpected message: {message}");

        Console.WriteLine(
            $"✔ Error message verified: {message}");
    }

    public async Task VerifyInvalidCustomerAccountAsync(string filePath)
    {
        Console.WriteLine();
        Console.WriteLine("========== INVALID CUSTOMER ACCOUNT ==========");

        // Select Excel file
        await Page.Locator(FileUpload)
            .SetInputFilesAsync(filePath);

        Console.WriteLine(
            $"✔ Excel file selected: {Path.GetFileName(filePath)}");

        // Click Upload
        await Page.Locator(UploadButton)
            .ClickAsync();

        Console.WriteLine("✔ Upload button clicked.");

        // Verify message
        var messageElement =
            Page.Locator(Message);

        await messageElement.WaitForAsync();

        var message =
            (await messageElement.InnerTextAsync()).Trim();

        Assert.AreEqual(
            "Customer account number does not exist",
            message,
            $"Unexpected message: {message}");

        Console.WriteLine(
            $"✔ Invalid customer account message verified: {message}");
    }
}