using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class CustomerGroupMaintainTests : BaseTest
{
    [TestMethod]
    public async Task Customer_Group_Maintain_Page_Loads()
    {
        await StartPortalSessionAsync("IMPL");

        var customerGroupPage =
            new CustomerGroupMaintainPage(Page);

        // =====================================================
        // Navigate
        // =====================================================

        await customerGroupPage.NavigateAsync();

        // =====================================================
        // Verify Page
        // =====================================================

        await customerGroupPage
            .VerifyCustomerGroupMaintainPageAsync();

        // =====================================================
        // Verify Form
        // =====================================================

        await customerGroupPage
            .VerifyCustomerGroupFormAsync();

        // =====================================================
        // Verify Merchant Group
        // =====================================================

        await customerGroupPage
            .VerifyMerchantGroupAsync();

        // =====================================================
        // Verify Required Field Validation
        // =====================================================

        await customerGroupPage
            .VerifyRequiredFieldValidationAsync();

        // =====================================================
        // Generate Unique Test Data
        // =====================================================

        var dateSuffix =
            DateTime.Now.ToString("ddMMyyHHmmss");

        var code =
            $"TestGroup{dateSuffix}";

        var description =
            $"AutomatedTest{dateSuffix}";

        var accountPrefix =
            $"AT{dateSuffix}";

        // =====================================================
        // Create Customer Group
        // =====================================================

        await customerGroupPage.CreateCustomerGroupAsync(
            "6687",
            code,
            description,
            accountPrefix);

        // =====================================================
        // Search Customer Group
        // =====================================================

        await customerGroupPage
            .SearchCustomerGroupAsync(description);

        // =====================================================
        // Open Customer Group
        // =====================================================

        await customerGroupPage
            .OpenCustomerGroupDetailsAsync();

        // =====================================================
        // Update Customer Group
        // =====================================================

        var updatedDescription =
            $"AutoTest{dateSuffix}";

        await customerGroupPage
            .UpdateDescriptionAsync(updatedDescription);

        Console.WriteLine();
        Console.WriteLine(
            "✔ Customer Group Maintain test completed successfully.");
    }
}