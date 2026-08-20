using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class CustomerGroupMaintainPage : BasePage
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

    private const string SettingsBreadcrumb =
        "a.breadcrumb.breadbtn2";

    // =====================================================
    // Form Locators
    // =====================================================

    private const string MerchantGroupLabel =
        "label.col-lg-3.control-label:nth-of-type(1)";

    private const string ParentCustomerGroupLabel =
        "label.col-lg-3.control-label:nth-of-type(2)";

    private const string CodeLabel =
        "label.col-lg-3.control-label:nth-of-type(3)";

    private const string DescriptionLabel =
        "label.col-lg-3.control-label:nth-of-type(4)";

    private const string AccountPrefixLabel =
        "label.col-lg-3.control-label:nth-of-type(5)";

    private const string MerchantGroupValue =
        "#cphBody_lblselMerchantGroup";

    private const string ParentCustomerGroupDropdown =
        "#cphBody_selParentCustomerGroup";

    private const string CodeTextBox =
        "#cphBody_txtCode";

    private const string DescriptionTextBox =
        "#cphBody_txtDescription";

    private const string AccountPrefixTextBox =
        "#cphBody_txtAccountPrefix";

    private const string SaveButton =
        "#btn-save-fake";

    // Validation messages
    private const string RequiredMessages =
        "div.col-lg-2";

    // =====================================================
    // Customer Group List
    // =====================================================

    private const string SearchInput =
        "input[type='search']";

    private const string GroupDetailsLink =
        "a[data-original-title='Group Details']";

    // =====================================================
    // Constructor
    // =====================================================

    public CustomerGroupMaintainPage(IPage page)
        : base(page)
    {
    }

    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await Page.GotoAsync(
            "https://admin-stage.payment24.co/CustomerGroupMaintain.aspx");

        await Page.WaitForLoadStateAsync(
            LoadState.DOMContentLoaded);
    }

    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyCustomerGroupMaintainPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine("=====================================");
        Console.WriteLine("VERIFYING CUSTOMER GROUP MAINTAIN PAGE");
        Console.WriteLine("=====================================");

        // Customer Management icon
        Assert.IsTrue(
            await Page.Locator(CustomerManagementIcon).IsVisibleAsync(),
            "Customer Management icon is not visible.");

        Console.WriteLine(
            "✔ Customer Management icon verified.");

        // Page heading
        var heading = Page.Locator(PageHeading);

        await heading.WaitForAsync();

        Assert.AreEqual(
            "Maintain Customer Group",
            (await heading.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Maintain Customer Group heading verified.");

        // Customers breadcrumb
        var customersBreadcrumb =
            Page.Locator(CustomersBreadcrumb);

        Assert.IsTrue(
            await customersBreadcrumb.IsVisibleAsync(),
            "Customers breadcrumb is not visible.");

        Console.WriteLine(
            "✔ Customers breadcrumb verified.");

        // Settings breadcrumb
        var settingsBreadcrumb =
            Page.Locator(SettingsBreadcrumb);

        Assert.AreEqual(
            "Settings",
            (await settingsBreadcrumb.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Settings breadcrumb verified.");
    }

    // =====================================================
    // Form Verification
    // =====================================================

    public async Task VerifyCustomerGroupFormAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CUSTOMER GROUP FORM ==========");

        var expectedLabels = new[]
        {
            "Merchant Group",
            "Parent Customer Group",
            "Code",
            "Description",
            "Account Prefix"
        };

        var labels = Page.Locator(
            "label.col-lg-3.control-label");

        var labelCount = await labels.CountAsync();

        Assert.IsTrue(
            labelCount >= expectedLabels.Length,
            $"Expected at least {expectedLabels.Length} labels but found {labelCount}.");

        for (var i = 0; i < expectedLabels.Length; i++)
        {
            var actualText =
                (await labels.Nth(i).InnerTextAsync()).Trim();

            Assert.AreEqual(
                expectedLabels[i],
                actualText,
                $"Unexpected label at position {i + 1}.");

            Console.WriteLine(
                $"✔ {actualText}");
        }
    }

    // =====================================================
    // Merchant Group Verification
    // =====================================================

    public async Task VerifyMerchantGroupAsync()
    {
        var merchantGroup =
            Page.Locator(MerchantGroupValue);

        await merchantGroup.WaitForAsync();

        var merchantGroupValue =
            (await merchantGroup.InnerTextAsync()).Trim();

        Assert.AreEqual(
            "Imperial",
            merchantGroupValue,
            $"Expected merchant group 'Imperial' but found '{merchantGroupValue}'.");

        Console.WriteLine(
            $"✔ Merchant Group populated: {merchantGroupValue}");
    }

    // =====================================================
    // Validation
    // =====================================================

    public async Task VerifyRequiredFieldValidationAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== REQUIRED FIELD VALIDATION ==========");

        await Page.Locator(SaveButton).ClickAsync();

        Console.WriteLine(
            "✔ Save button clicked with empty fields.");

        var requiredMessages =
            Page.Locator(RequiredMessages)
                .Filter(new()
                {
                    HasText = "* Required"
                });

        await requiredMessages.First.WaitForAsync();

        var count =
            await requiredMessages.CountAsync();

        Assert.IsTrue(
            count >= 2,
            $"Expected at least 2 required-field messages but found {count}.");

        Console.WriteLine(
            $"✔ Required-field validation displayed: {count} messages.");
    }

    // =====================================================
    // Create Customer Group
    // =====================================================

    public async Task CreateCustomerGroupAsync(
        string parentCustomerGroup,
        string code,
        string description,
        string accountPrefix)
    {
        Console.WriteLine();
        Console.WriteLine("========== CREATE CUSTOMER GROUP ==========");

        await Page.Locator(ParentCustomerGroupDropdown)
            .SelectOptionAsync(parentCustomerGroup);

        Console.WriteLine(
            $"✔ Parent Customer Group selected: {parentCustomerGroup}");

        await Page.Locator(CodeTextBox)
            .FillAsync(code);

        Console.WriteLine(
            $"✔ Code entered: {code}");

        await Page.Locator(DescriptionTextBox)
            .FillAsync(description);

        Console.WriteLine(
            $"✔ Description entered: {description}");

        await Page.Locator(AccountPrefixTextBox)
            .FillAsync(accountPrefix);

        Console.WriteLine(
            $"✔ Account Prefix entered: {accountPrefix}");

        await Page.Locator(SaveButton)
            .ClickAsync();

        Console.WriteLine(
            "✔ Customer Group Save button clicked.");
    }

    // =====================================================
    // Search Customer Group
    // =====================================================

    public async Task SearchCustomerGroupAsync(
        string description)
    {
        Console.WriteLine();
        Console.WriteLine("========== SEARCH CUSTOMER GROUP ==========");

        var searchInput =
            Page.Locator(SearchInput);

        await searchInput.WaitForAsync();

        await searchInput.FillAsync(description);

        Console.WriteLine(
            $"✔ Customer Group searched: {description}");
    }

    // =====================================================
    // Open Customer Group Details
    // =====================================================

    public async Task OpenCustomerGroupDetailsAsync()
    {
        var groupDetails =
            Page.Locator(GroupDetailsLink).First;

        await groupDetails.WaitForAsync();

        await groupDetails.ClickAsync();

        Console.WriteLine(
            "✔ Customer Group details opened.");
    }

    // =====================================================
    // Edit Customer Group
    // =====================================================

    public async Task UpdateDescriptionAsync(
        string newDescription)
    {
        Console.WriteLine();
        Console.WriteLine("========== UPDATE CUSTOMER GROUP ==========");

        var description =
            Page.Locator(DescriptionTextBox);

        await description.WaitForAsync();

        await description.FillAsync(newDescription);

        Console.WriteLine(
            $"✔ Description updated: {newDescription}");

        await Page.Locator(SaveButton)
            .ClickAsync();

        Console.WriteLine(
            "✔ Customer Group updated successfully.");
    }
}