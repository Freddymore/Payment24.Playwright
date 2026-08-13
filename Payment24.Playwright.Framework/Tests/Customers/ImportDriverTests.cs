using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Core.TestData;
using Payment24.Playwright.Framework.Pages.Customers;
using Payment24.Playwright.Framework.Core.TestData;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class ImportDriversTests : BaseTest
{
    [TestMethod]
    public async Task Import_Drivers_Page_Loads()
    {
        await StartPortalSessionAsync("IMPL");

        var importDriversPage = new ImportDriversPage(Page);

        await importDriversPage.NavigateAsync();

        await importDriversPage.VerifyImportDriversPageAsync();

        await importDriversPage.VerifyImportDriversFormAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Import Drivers page smoke test completed successfully.");
    }

    [TestMethod]
    public async Task Import_Drivers_With_Valid_Template()
    {
        await StartPortalSessionAsync("IMPL");

        var importDriversPage = new ImportDriversPage(Page);

        await importDriversPage.NavigateAsync();

        await importDriversPage.VerifyImportDriversPageAsync();

        await importDriversPage.VerifyImportDriversFormAsync();

        var templatePath = Path.Combine(
            AppContext.BaseDirectory,
            "TestData",
            "Templates",
            "BulkDriversUpload_Template.xlsx");

        var generatedTemplate =
            DriverTemplateHelper.CreateUniqueDriverTemplate(templatePath);

        await importDriversPage.ImportValidDriversTemplateAsync(
            generatedTemplate);

        Console.WriteLine();
        Console.WriteLine(
            "✔ Valid drivers template test completed successfully.");
    }
}