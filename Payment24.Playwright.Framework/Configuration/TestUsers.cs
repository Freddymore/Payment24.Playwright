using Payment24.Playwright.Framework.Models;

namespace Payment24.Playwright.Framework.Configuration;

public static class TestUsers
{
    public static PortalUser GetUser(string merchant)
    {
        var section = ConfigManager.Configuration.GetSection($"Users:{merchant}");

        return new PortalUser
        {
            MerchantCode = section["MerchantCode"]!,
            Username = section["Username"]!,
            Password = section["Password"]!,
            Logo = section["Logo"]!       // NEW
        };
    }
}