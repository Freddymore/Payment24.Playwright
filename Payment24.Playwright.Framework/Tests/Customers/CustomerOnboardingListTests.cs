using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class CustomerOnboardingListTests : BaseTest
{
    [TestMethod]
    public async Task Customer_Onboarding_List_Page_Loads()
    {
        await StartPortalSessionAsync("SAT");

        var customerOnboardingListPage =
            new CustomerOnboardingListPage(Page);

        await customerOnboardingListPage.NavigateAsync();

        await customerOnboardingListPage
            .VerifyCustomerOnboardingListPageAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Customer Onboarding List page smoke test completed successfully.");
    }
}