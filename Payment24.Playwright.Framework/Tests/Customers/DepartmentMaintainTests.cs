using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class DepartmentMaintainTests : BaseTest
{
    [TestMethod]
    public async Task Department_Maintain_Page_Loads()
    {
        await StartPortalSessionAsync("IMPL");

        var departmentPage =
            new DepartmentMaintainPage(Page);

        await departmentPage.NavigateAsync();

        await departmentPage.VerifyDepartmentPageAsync();

        await departmentPage.VerifyPageLoadTimeAsync();

        await departmentPage.OpenAddDepartmentFormAsync();

        await departmentPage.VerifyDepartmentFormAsync();

        await departmentPage.PopulateAndSaveDepartmentAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Department Maintain test completed successfully.");
    }
}