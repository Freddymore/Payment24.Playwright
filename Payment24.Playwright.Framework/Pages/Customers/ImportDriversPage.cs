using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class ImportDriversPage : BasePage
{
    // =====================================================
    // Page Locators
    // =====================================================

    private const string CustomerManagementIcon =
        "img[src='images/icons/customer_management_icon@2x.png']";

    private const string PageHeading =
        "h2.heading";

    private const string CustomersBreadcrumb =
        "a.breadcontainer.breadbtn1";

    private const string ImportDriversBreadcrumb =
        "a.breadcrumb.breadbtn2";


    // =====================================================
    // Import Drivers Form
    // =====================================================

    private const string SendPinLabel =
    "label.control-label:nth-of-type(1)";

    private const string StatusLabel =
        "label.control-label:nth-of-type(2)";

    private const string StatusDropdown =
        "#cphBody_selStatus";

    private const string FileUpload =
        "#cphBody_FileUpload1";

    private const string ViewButton =
        "#cphBody_btnView";

    private const string UploadButton =
        "#cphBody_btnUpload";

    private const string DriversGrid =
        "#cphBody_gridDrivers";

    private const string SuccessMessage =
        "p.alert-message";

    // =====================================================
    // Constructor
    // =====================================================

    public ImportDriversPage(IPage page)
        : base(page)
    {
    }


    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await Page.GotoAsync(
            "https://admin-stage.payment24.co/ImportDrivers.aspx");

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);
    }


    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyImportDriversPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=====================================");
        Console.WriteLine("VERIFYING IMPORT DRIVERS PAGE");
        Console.WriteLine("=====================================");

        // Verify Customer Management icon
        Assert.IsTrue(
            await Page.Locator(CustomerManagementIcon).IsVisibleAsync(),
            "Customer Management icon is not visible.");

        Console.WriteLine("✔ Customer Management icon verified.");


        // Verify page heading
        var heading = Page.Locator(PageHeading);

        await heading.WaitForAsync();

        Assert.AreEqual(
            "Import Drivers",
            (await heading.InnerTextAsync()).Trim());

        Console.WriteLine("✔ Import Drivers heading verified.");


        // Verify Customers breadcrumb
        var customersBreadcrumb =
            Page.Locator(CustomersBreadcrumb);

        Assert.IsTrue(
            await customersBreadcrumb.IsVisibleAsync(),
            "Customers breadcrumb is not visible.");

        Console.WriteLine("✔ Customers breadcrumb verified.");


        // Verify Import Drivers breadcrumb
        var importDriversBreadcrumb =
            Page.Locator(ImportDriversBreadcrumb);

        Assert.AreEqual(
            "Import Drivers",
            (await importDriversBreadcrumb.InnerTextAsync()).Trim());

        Console.WriteLine("✔ Import Drivers breadcrumb verified.");
    }

    public async Task VerifyImportDriversFormAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== IMPORT DRIVERS FORM ==========");

        // Verify Send PIN label
        var sendPinLabel = Page.Locator(SendPinLabel);

        Assert.AreEqual(
            "Send PIN",
            (await sendPinLabel.InnerTextAsync()).Trim());

        Console.WriteLine("✔ Send PIN field verified.");

        // Verify Status label
        var statusLabel = Page.Locator(StatusLabel);

        Assert.AreEqual(
            "Status",
            (await statusLabel.InnerTextAsync()).Trim());

        Console.WriteLine("✔ Status field verified.");

        // Verify Status dropdown
        var statusDropdown = Page.Locator(StatusDropdown);

        Assert.IsTrue(
            await statusDropdown.IsVisibleAsync(),
            "Status dropdown is not visible.");



        // Count available status options
        var statusOptions = statusDropdown.Locator("option");

        var statusCount = await statusOptions.CountAsync();

        Assert.AreEqual(
            9,
            statusCount,
            $"Expected 9 status options but found {statusCount}.");

        Console.WriteLine(
            $"✔ Status dropdown contains {statusCount} options.");
    }

    public async Task ImportValidDriversTemplateAsync(string filePath)
    {
        Console.WriteLine();
        Console.WriteLine("========== VALID DRIVER IMPORT ==========");

        // Select Excel template
        await Page.Locator(FileUpload)
            .SetInputFilesAsync(filePath);

        Console.WriteLine("✔ Driver template selected.");

        // Click View
        await Page.Locator(ViewButton)
            .ClickAsync();

        Console.WriteLine("✔ View button clicked.");

        // Wait for the driver grid
        var driversGrid = Page.Locator(DriversGrid);

        await driversGrid.WaitForAsync();

        Console.WriteLine("✔ Driver import grid displayed.");

        // Get first data row
        var dataRow = driversGrid
            .Locator("tbody tr")
            .Nth(1);

        await dataRow.WaitForAsync();

        Console.WriteLine("✔ Driver data row displayed.");

        // Read Feedback column
        var feedback = (
            await dataRow
                .Locator("td")
                .Nth(11)
                .InnerTextAsync()
        ).Trim();

        Console.WriteLine($"✔ Feedback: {feedback}");

        // The driver must pass validation before upload
        Assert.IsTrue(
            string.IsNullOrWhiteSpace(feedback),
            $"Driver import validation failed: {feedback}");

        Console.WriteLine("✔ Driver data passed validation.");

        // Click Upload
        await Page.Locator(UploadButton)
            .ClickAsync();

        Console.WriteLine("✔ Upload button clicked.");

        // Verify success message
        var successMessage =
            Page.Locator(SuccessMessage);

        await successMessage.WaitForAsync();

        var message =
            (await successMessage.InnerTextAsync()).Trim();

        Assert.AreEqual(
            "Drivers imported successfully.",
            message,
            $"Unexpected import message: {message}");

        Console.WriteLine(
            $"✔ Import successful: {message}");
    }
}
