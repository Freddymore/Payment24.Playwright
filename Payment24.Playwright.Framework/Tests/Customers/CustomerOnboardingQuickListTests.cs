using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class CustomerOnboardingQuickListTests : BaseTest
{
    [TestMethod]
    public async Task Customer_Onboarding_QuickList_Page_Loads()
    {
        await StartPortalSessionAsync("SAT");

        var customerOnboardingQuickListPage =
            new CustomerOnboardingQuickListPage(Page);

        await customerOnboardingQuickListPage.NavigateAsync();

        await customerOnboardingQuickListPage
            .VerifyCustomerOnboardingQuickListPageAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Customer Onboarding Quicklist page smoke test completed successfully.");
    }
}