using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment24.Playwright.Framework.Core;
using Payment24.Playwright.Framework.Pages.Customers;

namespace Payment24.Playwright.Framework.Tests.Customers;

[TestClass]
public class DriverInviteTests : BaseTest
{
    [TestMethod]
    public async Task Driver_Invite_Page_Loads()
    {
        await StartPortalSessionAsync("BPCRT");

        var driverInvitePage = new DriverInvitePage(Page);

        await driverInvitePage.NavigateAsync();

        await driverInvitePage.VerifyDriverInvitePageAsync();

        Console.WriteLine();
        Console.WriteLine(
            "✔ Driver Invite page smoke test completed successfully.");
    }
}