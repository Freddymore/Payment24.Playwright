using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Pages.Components;

namespace Payment24.Playwright.Framework.Pages.Dashboard;

public class DashboardPage : BasePage
{
    // ============================
    // Locators
    // ============================

    // Update this selector after inspecting your dashboard
    private const string DashboardHeader = "h2.heading";

    public DashboardPage(IPage page) : base(page)
    {
    }

    // ============================
    // Verification
    // ============================

    /// <summary>
    /// Returns true if the dashboard has loaded.
    /// </summary>
    public async Task<bool> IsDashboardDisplayedAsync()
    {
        return await IsVisibleAsync(DashboardHeader);
    }

    /// <summary>
    /// Verifies the dashboard loaded successfully.
    /// </summary>
    public async Task VerifyDashboardLoadedAsync()
    {
        await WaitForPageLoadAsync();

        Assert.IsTrue(
            await IsDashboardDisplayedAsync(),
            "Dashboard failed to load.");

        Console.WriteLine("✔ Dashboard loaded successfully.");
    }

    /// <summary>
    /// Accept cookies if the banner is displayed.
    /// </summary>
    public async Task AcceptCookiesAsync()
    {
        var cookieBanner = new CookieBannerComponent(Page);

        await cookieBanner.AcceptIfDisplayedAsync();
    }

    /// <summary>
    /// Measures dashboard load time.
    /// </summary>
    public async Task VerifyDashboardResponseTimeAsync(int maxMilliseconds = 15000)
    {
        var loadTime = await Page.EvaluateAsync<int>(
            @"() => performance.timing.domContentLoadedEventEnd -
                   performance.timing.navigationStart");

        Assert.IsTrue(
            loadTime <= maxMilliseconds,
            $"Dashboard loaded in {loadTime} ms which exceeds {maxMilliseconds} ms.");

        Console.WriteLine($"✔ Dashboard loaded in {loadTime} ms");
    }
}