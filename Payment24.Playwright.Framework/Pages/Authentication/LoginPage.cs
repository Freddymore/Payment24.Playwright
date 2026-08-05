using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Configuration;
using Payment24.Playwright.Framework.Models;

namespace Payment24.Playwright.Framework.Pages.Authentication;

public class LoginPage : BasePage
{
    // =========================
    // Locators
    // =========================

    private const string UsernameTextBox = "#txtUserName";
    private const string PasswordTextBox = "#txtPassword";
    private const string LoginButton = "#BtnLogin";

    // Merchant Logo
    private const string MerchantLogo = "img.tilt.p24img";

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
    // Complete Login Workflow
    // =========================

    public async Task LoginToMerchantAsync(string merchant)
    {
        // Read merchant configuration
        var user = TestUsers.GetUser(merchant);

        // Navigate to merchant login page
        await NavigateToLoginPageAsync(
            $"{ConfigManager.Portal.BaseUrl}/Login.aspx?code={user.MerchantCode}");

        // Verify login page
        Assert.IsTrue(
            await IsLoginPageDisplayedAsync(),
            "Login page was not displayed.");

        // Verify merchant branding
        await VerifyLogoAsync(user.Logo);

        // Perform login
        await LoginAsync(user);

        Console.WriteLine($"✔ Logged into merchant: {merchant}");
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
        var logo = Page.Locator(MerchantLogo);

        await logo.WaitForAsync();

        var logoSource = await logo.GetAttributeAsync("src");

        Assert.IsNotNull(
            logoSource,
            "Merchant logo src attribute is null.");

        Assert.IsTrue(
            logoSource.Contains(expectedLogo, StringComparison.OrdinalIgnoreCase),
            $"Expected logo '{expectedLogo}' but found '{logoSource}'.");

        Console.WriteLine("✔ Merchant logo verified successfully.");
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