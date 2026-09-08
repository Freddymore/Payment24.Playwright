using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Configuration;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Models;
using Payment24.Playwright.Framework.Pages;
using Payment24.Playwright.Framework.Pages.Authentication;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class  DriverMaintainTests : BaseTest
{
    [TestMethod]
    public async Task Driver_Maintain_Page_Loads()
    {
        await StartPortalSessionAsync("BPCRT");
        var driverMaintainPage = new DriverMaintainPage(Page);


        // =====================================================
        // Navigate
        // =====================================================

        //await driverMaintainPage.NavigateAsync();


        // ========================================================
        // DRIVER LIST
        // ========================================================

        await driverMaintainPage.NavigateToDriverListAsync();
        Console.WriteLine(
            "✔ Driver List page loaded.");

        // ========================================================
        // SELECT FLEET
        // ========================================================

        await driverMaintainPage.SelectFleetAsync(
            "DO NOT DELETE(BP HO)");
        Console.WriteLine(
            "✔ Fleet selected: DO NOT DELETE(BP HO)");

        // ========================================================
        // ADD DRIVER
        // ========================================================

        await driverMaintainPage.ClickAddDriverAsync();
        Console.WriteLine(
            "✔ Add Driver button clicked.");

        // ========================================================
        // DRIVER MANAGEMENT PAGE
        // ========================================================

        await driverMaintainPage.VerifyDriverManagementPageAsync();

        // ========================================================
        // DETAILS
        // ========================================================

        await driverMaintainPage.VerifyDetailsTabAsync();
        await driverMaintainPage.VerifyNavigationButtonsAsync();

        // Verify mandatory fields
        await driverMaintainPage.VerifyRequiredValidationAsync();

        // Enter Details data
        await driverMaintainPage.FillDetailsAsync();

        // ========================================================
        // OTHER INFORMATION
        // ========================================================

        await driverMaintainPage.OpenOtherInformationAsync();
        await driverMaintainPage.VerifyOtherInformationFieldsAsync();
        await driverMaintainPage.FillOtherInformationAsync();

        // ========================================================
        // ALLOWED LOCATIONS
        // ========================================================

        await driverMaintainPage.OpenAllowedLocationsAsync();
        await driverMaintainPage.VerifyAllowedLocationsFieldsAsync();
        await driverMaintainPage.FillAllowedLocationsAsync();

        // ========================================================
        // FILLING RULES AND LIMITS
        // ========================================================

        await driverMaintainPage.OpenFillingRulesAndLimitsAsync();
        await driverMaintainPage.VerifyFillingRulesAndLimitsFieldsAsync();
        await driverMaintainPage.VerifyCurrencyAndMeasurementsAsync();

        // ========================================================
        // AUDIT TRAIL
        // ========================================================

        await driverMaintainPage.OpenAuditTrailAsync();
        await driverMaintainPage.VerifyAuditTrailFieldsAsync();

        // ========================================================
        // SAVE
        // ========================================================

        await driverMaintainPage.SaveDriverAsync();
        Console.WriteLine("");
        Console.WriteLine("==========================================");
        Console.WriteLine(" DRIVER MAINTAIN TEST COMPLETED SUCCESSFULLY");
        Console.WriteLine("==========================================");
    }
    
}

