using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class CustomerGroupListTests : BaseTest
{
    [TestMethod]
    public async Task Customer_Group_List_Page_Loads()
    {
        await StartPortalSessionAsync("IMPL");

        var customerGroupListPage =
            new CustomerGroupListPage(Page);

        await customerGroupListPage.NavigateAsync();

        await customerGroupListPage.VerifyCustomerGroupListPageAsync();

        await customerGroupListPage.VerifyCustomerGroupListControlsAsync();

        await customerGroupListPage.VerifyCustomerGroupColumnsAsync();

        await customerGroupListPage.VerifyCustomerGroupDataTypesAsync();

        await customerGroupListPage.VerifyGroupDetailsActionAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Customer Group List page smoke test completed successfully.");
    }
}