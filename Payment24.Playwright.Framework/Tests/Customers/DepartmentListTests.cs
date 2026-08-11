using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class DepartmentListTests : BaseTest
{
    [TestMethod]
    public async Task Department_List_Page_Loads()
    {
        await StartPortalSessionAsync("IMPL");

        var departmentPage = new DepartmentListPage(Page);

        await departmentPage.NavigateAsync();

        await departmentPage.VerifyDepartmentPageAsync();

        Console.WriteLine();
        Console.WriteLine("✔ Department page smoke test completed successfully.");
    }
}