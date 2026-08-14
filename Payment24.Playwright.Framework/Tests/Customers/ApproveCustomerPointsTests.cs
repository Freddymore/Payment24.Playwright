using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class ApproveCustomerPointsTests : BaseTest
{
    [TestMethod]
    public async Task Approve_Customer_Points_Page_Loads()
    {
        await StartPortalSessionAsync("MPAY");

        var approveCustomerPointsPage =
            new ApproveCustomerPointsPage(Page);

        await approveCustomerPointsPage.NavigateAsync();

        await approveCustomerPointsPage
            .VerifyApproveCustomerPointsPageAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Approve Customer Points page smoke test completed successfully.");
    }
}