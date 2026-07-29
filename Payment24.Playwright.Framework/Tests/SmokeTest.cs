using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;

namespace Payment24.Playwright.Framework.Tests;

[TestClass]
public class SmokeTest : BaseTest
{
    [TestMethod]
    public async Task Login_To_IMPL_Merchant()
    {
        await StartPortalSessionAsync("IMPL");

        Assert.IsFalse(
            Page.Url.Contains("Login.aspx"),
            "Login failed. User is still on the Login page.");

        Console.WriteLine($"Logged in successfully.");
        Console.WriteLine($"Current Page: {Page.Url}");
    }
}