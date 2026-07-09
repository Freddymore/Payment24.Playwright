using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Models;

namespace Payment24.Playwright.Framework.Pages;

public class LoginPage : BasePage
{
    // =========================
    // Locators
    // =========================

    private const string UsernameTextBox = "#txtUserName";
    private const string PasswordTextBox = "#txtPassword";
    private const string LoginButton = "#BtnLogin";

    // Matches all merchant logos (RUBELogo, TOTALLogo, IMPLLogo, etc.)
    private const string MerchantLogo = "#imgHeader";

    public LoginPage(IPage page) : base(page)
    {
    }

    // =========================
    // Navigation
    // =========================

    public async Task NavigateToLoginPageAsync(string url)
    {
        await NavigateToAsync(url);
    }

    // =========================
    // Verification
    // =========================

    public async Task<bool> IsLoginPageDisplayedAsync()
    {
        return await IsVisibleAsync(UsernameTextBox);
    }

    public async Task VerifyLogoAsync(string expectedLogo)
    {
        var logo = Page.Locator("img.tilt.p24img");

        // Verify the image is visible
        await Assertions.Expect(logo).ToBeVisibleAsync();

        // Get the src attribute
        var logoSource = await logo.GetAttributeAsync("src");

        Assert.IsNotNull(logoSource, "Merchant logo src attribute is null.");

        Assert.IsTrue(
            logoSource.Contains(expectedLogo, StringComparison.OrdinalIgnoreCase),
            $"Expected logo '{expectedLogo}' but found '{logoSource}'.");

        Console.WriteLine($"✔ Merchant logo verified successfully.");
        Console.WriteLine($"   Logo Source: {logoSource}");
    }

    // =========================
    // Login
    // =========================

    public async Task LoginAsync(PortalUser user)
    {
        await FillAsync(UsernameTextBox, user.Username);

        await FillAsync(PasswordTextBox, user.Password);

        await ClickAsync(LoginButton);

        await WaitForPageLoadAsync();
    }
}