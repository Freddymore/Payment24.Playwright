using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class CustomerListTests : BaseTest
{
    [TestMethod]
    public async Task Customer_List_Page_Loads()
    {
        // Login
        await StartPortalSessionAsync("IMPL");

        // Open Customer page
        var customerPage = new CustomerListPage(Page);

        await customerPage.NavigateAsync();

        // Verify complete page
        await customerPage.VerifyCustomerPageAsync();

        Console.WriteLine();
        Console.WriteLine("✔ Customer page smoke test completed successfully.");
    }
}