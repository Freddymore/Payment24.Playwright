using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Payment24.Playwright.Framework.Pages.Customers;

public class DepartmentMaintainPage : BasePage
{
    // =====================================================
    // URL
    // =====================================================

    private const string DepartmentListUrl =
        "/DepartmentList.aspx";


    // =====================================================
    // Main Page
    // =====================================================

    private const string PageHeading =
        "h2.heading";

    private const string AddDepartmentButton =
        "#cphBody_btnAdd";


    // =====================================================
    // Department Form
    // =====================================================

    private const string AccountName =
        "#select2-cphBody_selFleet-container";

    private const string AccountNameSearch =
        ".select2-container--open input[type='search']";

    private const string AccountNameResults =
        "#select2-cphBody_selFleet-results li";

    private const string DepartmentName =
        "#cphBody_txtName";

    private const string ParentDepartment =
        "#select2-cphBody_selParentDepartment-container";

    private const string ParentDepartmentResults =
        "#select2-cphBody_selParentDepartment-results li";

    private const string CostCentre =
        "#select2-cphBody_selCostCentre-container";

    private const string CostCentreResults =
        "#select2-cphBody_selCostCentre-results li";

    private const string Region =
        "#cphBody_selRegion";

    private const string CreditLimit =
        "#cphBody_txtCreditLimit";

    private const string CreditLimitType =
        "#cphBody_selCreditLimitType";

    private const string AvailableBalance =
        "#cphBody_txtAvailableBalance";

    private const string ContactPerson =
        "#cphBody_txtContactPerson";

    private const string ContactNumber =
        "#cphBody_txtContactNumber";


    // =====================================================
    // Radio Buttons
    // =====================================================

    private const string IsActiveRadio =
        "input[type='checkbox']";

    private const string CheckCreditLimitRadio =
        "label[for='cphBody_chkCreditLimit']";


    // =====================================================
    // Country Code
    // =====================================================

    private const string CountryFlag =
        ".iti__selected-flag";

    private const string SouthAfricaCode =
        "li[data-dial-code='27']";


    // =====================================================
    // Save
    // =====================================================

    private const string SaveButton =
        "button[type='submit']";


    // =====================================================
    // Constructor
    // =====================================================

    public DepartmentMaintainPage(IPage page)
        : base(page)
    {
    }


    // =====================================================
    // Navigation
    // =====================================================

    public async Task NavigateAsync()
    {
        await NavigateToAsync(DepartmentListUrl);
    }

    // =====================================================
    // Page Response Time
    // =====================================================

    public async Task VerifyPageLoadTimeAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        await WaitForPageLoadAsync();

        stopwatch.Stop();

        var loadTime = stopwatch.ElapsedMilliseconds;

        Assert.IsTrue(
            loadTime <= 15000,
            $"Department List page response time ({loadTime} ms) exceeds the threshold of 15000 ms.");

        Console.WriteLine(
            $"Department List page loaded in {loadTime} ms");
    }

    // =====================================================
    // Open Add Department Form
    // =====================================================

    public async Task OpenAddDepartmentFormAsync()
    {
        var addButton =
            Page.Locator(AddDepartmentButton);

        await addButton.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        await addButton.ClickAsync();

        Console.WriteLine(
            "✔ Add Department form opened.");
    }


    // =====================================================
    // Page Verification
    // =====================================================

    public async Task VerifyDepartmentPageAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "==============================================");
        Console.WriteLine(
            "VERIFYING DEPARTMENT LIST PAGE");
        Console.WriteLine(
            "==============================================");

        await WaitForPageLoadAsync();

        var heading =
            Page.Locator(PageHeading);

        await heading.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 15000
            });

        Assert.AreEqual(
            "Departments",
            (await heading.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Departments heading verified.");
    }


    // =====================================================
    // Verify Form Labels
    // =====================================================

    public async Task VerifyDepartmentFormAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "========== DEPARTMENT FORM ==========");

        string[] expectedLabels =
        {
            //"",
            "Account Number",
            "Departments",
            "Parent Department",
            "Cost Centre",
            "Region",
            "Available Balance",
            "Contact Person",
            "Contact Number",
            "Email"
            //""
        };

        var labels =
            Page.Locator(".form-group label");

        var count =
            await labels.CountAsync();

        Assert.IsTrue(
            count >= expectedLabels.Length,
            $"Expected at least {expectedLabels.Length} labels but found {count}.");

        for (int i = 0; i < expectedLabels.Length; i++)
        {
            var actual =
                (await labels.Nth(i).InnerTextAsync()).Trim();

            Assert.AreEqual(
                expectedLabels[i],
                actual,
                $"Department form label {i + 1} is incorrect.");

            Console.WriteLine(
                $"✔ {actual}");
        }


        // Check credit limit text
        var creditLimitLabel =
            Page.Locator("#cphBody_lblCreditLimit");

        Assert.AreEqual(
            "Check credit limit for new voucher",
            (await creditLimitLabel.InnerTextAsync()).Trim());

        Console.WriteLine(
            "✔ Check credit limit for new voucher verified.");
    }


    // =====================================================
    // Verify Is Active
    // =====================================================

    public async Task VerifyIsActiveAsync()
    {
        var isActive =
            Page.Locator("#cphBody_chkIsActive");

        await isActive.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Attached,
                Timeout = 10000
            });

        Assert.IsTrue(
            await isActive.IsEnabledAsync(),
            "Is Active control is not enabled.");

        if (!await isActive.IsCheckedAsync())
        {
            await isActive.CheckAsync();
        }

        Console.WriteLine(
            "✔ Is Active radio button is enabled and selected.");
    }


    // =====================================================
    // Verify Check Credit Limit
    // =====================================================

    public async Task VerifyCheckCreditLimitAsync()
    {
        Console.WriteLine("Checking credit limit option...");

        var checkCreditLimit =
            Page.Locator("#cphBody_chkCreditLimit");

        await checkCreditLimit.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Attached,
                Timeout = 10000
            });

        Assert.IsTrue(
            await checkCreditLimit.IsEnabledAsync(),
            "Check credit limit checkbox is not enabled.");

        if (!await checkCreditLimit.IsCheckedAsync())
        {
            await checkCreditLimit.CheckAsync();

            Console.WriteLine(
                "✔ Check credit limit checkbox selected.");
        }
        else
        {
            Console.WriteLine(
                "✔ Check credit limit checkbox already selected.");
        }

        // Allow any JavaScript associated with the checkbox
        // to render the Credit Limit controls.
        await Page.WaitForTimeoutAsync(1000);

        Console.WriteLine(
            "✔ Check credit limit for new voucher verified.");
    }


    // =====================================================
    // Account Name
    // =====================================================

    public async Task SelectAccountNameAsync()
    {
        await Page.Locator(AccountName).ClickAsync();

        var search =
            Page.Locator(AccountNameSearch);

        await search.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        await search.FillAsync("QA TEST");

        var result =
            Page.Locator(AccountNameResults).First;

        await result.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        await result.ClickAsync();

        Console.WriteLine(
            "✔ Account Name selected: QA TEST.");
    }


    // =====================================================
    // Department
    // =====================================================

    public async Task EnterDepartmentAsync()
    {
        await Page.Locator(DepartmentName)
            .FillAsync("DEF");

        Console.WriteLine(
            "✔ Department entered: DEF.");
    }


    // =====================================================
    // Parent Department
    // =====================================================

    public async Task SelectParentDepartmentAsync()
    {
        await Page.Locator(ParentDepartment).ClickAsync();

        var result =
            Page.Locator(ParentDepartmentResults).First;

        await result.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        await result.ClickAsync();

        Console.WriteLine(
            "✔ Parent Department selected.");
    }


    // =====================================================
    // Cost Centre
    // =====================================================

    public async Task SelectCostCentreAsync()
    {
        await Page.Locator(CostCentre).ClickAsync();

        var result =
            Page.Locator(CostCentreResults).Nth(1);

        await result.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        await result.ClickAsync();

        Console.WriteLine(
            "✔ Cost Centre selected.");
    }


    // =====================================================
    // Region
    // =====================================================

    public async Task SelectRegionAsync()
    {
        await Page.Locator(Region)
            .SelectOptionAsync("6");

        Console.WriteLine(
            "✔ Region selected: 6.");
    }


    // =====================================================
    // Credit Limit
    // =====================================================

    public async Task EnterCreditLimitAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CREDIT LIMIT DEBUG ==========");

        var creditLimit =
            Page.Locator("#cphBody_txtCreditLimit");

        var count =
            await creditLimit.CountAsync();

        Console.WriteLine(
            $"Credit Limit locator count: {count}");

        // Find anything on the page containing "Credit"
        var creditElements =
            Page.Locator("input, select, textarea, label");

        var total =
            await creditElements.CountAsync();

        Console.WriteLine(
            $"Total form elements found: {total}");

        for (int i = 0; i < total; i++)
        {
            var element = creditElements.Nth(i);

            var id =
                await element.GetAttributeAsync("id");

            var name =
                await element.GetAttributeAsync("name");

            var type =
                await element.GetAttributeAsync("type");

            var text =
                (await element.InnerTextAsync()).Trim();

            if ((id?.Contains("Credit",
                    StringComparison.OrdinalIgnoreCase) ?? false) ||
                (name?.Contains("Credit",
                    StringComparison.OrdinalIgnoreCase) ?? false) ||
                (text.Contains("Credit",
                    StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(
                    $"Credit-related element -> " +
                    $"Tag: {await element.EvaluateAsync<string>("e => e.tagName")} " +
                    $"ID: {id} " +
                    $"Name: {name} " +
                    $"Type: {type} " +
                    $"Text: {text}");
            }
        }

        Console.WriteLine(
            "=======================================");
    }

    // =====================================================
    // Credit Limit Type
    // =====================================================

    public async Task SelectCreditLimitTypeAsync()
    {
        Console.WriteLine();
        Console.WriteLine("========== CREDIT LIMIT TYPE DEBUG ==========");

        var creditLimitType =
            Page.Locator("#cphBody_selCreditLimitType");

        Console.WriteLine(
            $"Credit Limit Type count: {await creditLimitType.CountAsync()}");

        var inputs =
            Page.Locator("input, select, textarea");

        var count =
            await inputs.CountAsync();

        Console.WriteLine(
            $"Total input/select/textarea elements: {count}");

        for (int i = 0; i < count; i++)
        {
            var element = inputs.Nth(i);

            var tag =
                await element.EvaluateAsync<string>(
                    "e => e.tagName");

            var id =
                await element.GetAttributeAsync("id");

            var name =
                await element.GetAttributeAsync("name");

            var type =
                await element.GetAttributeAsync("type");

            if ((id?.Contains("Credit",
                    StringComparison.OrdinalIgnoreCase) ?? false) ||
                (name?.Contains("Credit",
                    StringComparison.OrdinalIgnoreCase) ?? false))
            {
                Console.WriteLine(
                    $"Element -> Tag: {tag}, " +
                    $"ID: {id}, " +
                    $"Name: {name}, " +
                    $"Type: {type}");
            }
        }

        Console.WriteLine(
            "============================================");
    }


    // =====================================================
    // Available Balance
    // =====================================================

    public async Task EnterAvailableBalanceAsync()
    {
        await Page.Locator(AvailableBalance)
            .FillAsync("50000");

        Console.WriteLine(
            "✔ Available Balance entered: 50000.");
    }


    // =====================================================
    // Contact Person
    // =====================================================

    public async Task EnterContactPersonAsync()
    {
        await Page.Locator(ContactPerson)
            .FillAsync("Payment24");

        Console.WriteLine(
            "✔ Contact Person entered: Payment24.");
    }


    // =====================================================
    // Contact Number
    // =====================================================

    public async Task EnterContactNumberAsync()
    {
        await Page.Locator(CountryFlag).ClickAsync();

        var southAfrica =
            Page.Locator(SouthAfricaCode);

        await southAfrica.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        await southAfrica.ClickAsync();

        await Page.Locator(ContactNumber)
            .FillAsync("661638043");

        Console.WriteLine(
            "✔ South Africa country code selected.");
        Console.WriteLine(
            "✔ Contact Number entered: 661638043.");
    }


    // =====================================================
    // Save
    // =====================================================

    public async Task SaveDepartmentAsync()
    {
        var saveButtons =
            Page.Locator(SaveButton);

        var count =
            await saveButtons.CountAsync();

        Assert.IsTrue(
            count >= 2,
            $"Expected at least 2 submit buttons but found {count}.");

        var saveButton =
            saveButtons.Nth(1);

        await saveButton.WaitForAsync(
            new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = 10000
            });

        await saveButton.ClickAsync();

        Console.WriteLine(
            "✔ Save Department button clicked.");
    }


    // =====================================================
    // Complete Workflow
    // =====================================================

    public async Task PopulateAndSaveDepartmentAsync()
    {
        Console.WriteLine();
        Console.WriteLine(
            "========== POPULATING DEPARTMENT ==========");

        await VerifyIsActiveAsync();

        await VerifyCheckCreditLimitAsync();

        await SelectAccountNameAsync();

        await EnterDepartmentAsync();

        await SelectParentDepartmentAsync();

        await SelectCostCentreAsync();

        await SelectRegionAsync();

        await EnterCreditLimitAsync();

        await SelectCreditLimitTypeAsync();

        await EnterAvailableBalanceAsync();

        await EnterContactPersonAsync();

        await EnterContactNumberAsync();

        await SaveDepartmentAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Department details populated and save submitted.");
    }
}