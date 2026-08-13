using ClosedXML.Excel;

namespace Payment24.Playwright.Framework.Core.TestData;

public static class DriverTemplateHelper
{
    public static string CreateUniqueDriverTemplate(string templatePath)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

        var outputDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "TestData",
            "Generated");

        Directory.CreateDirectory(outputDirectory);

        var outputPath = Path.Combine(
            outputDirectory,
            $"BulkDriversUpload_{timestamp}.xlsx");

        File.Copy(templatePath, outputPath, true);

        using var workbook = new XLWorkbook(outputPath);

        var worksheet = workbook.Worksheet("Driver_Information");

        // Generate unique test data
        var random = Random.Shared.Next(10_000_000, 99_999_999);

        var mobileNumber = $"278{random}";
        var firstName = "AutoTest";
        var surname = $"Driver{random}";

        // Driver_Information row 2
        worksheet.Cell(2, 1).Value = "27729053339";       // AccountNumber
        worksheet.Cell(2, 2).Value = "Default";           // Department
        worksheet.Cell(2, 3).Value = mobileNumber;        // MobileNumber
        worksheet.Cell(2, 4).Value = firstName;           // FirstName
        worksheet.Cell(2, 5).Value = surname;             // Surname
        worksheet.Cell(2, 6).Value =
            $"{firstName.ToLower()}.{surname.ToLower()}@payments24.com";
        worksheet.Cell(2, 7).Value = "YES";               // IsFleetManager
        worksheet.Cell(2, 8).Value = "1453";              // Pin

        // Optional fields
        worksheet.Cell(2, 9).Value = "";
        worksheet.Cell(2, 10).Value = "";
        worksheet.Cell(2, 11).Value = "";

        workbook.Save();

        Console.WriteLine();
        Console.WriteLine("========== TEST DRIVER DATA ==========");
        Console.WriteLine($"✔ Mobile Number: {mobileNumber}");
        Console.WriteLine($"✔ First Name: {firstName}");
        Console.WriteLine($"✔ Surname: {surname}");
        Console.WriteLine($"✔ Generated template: {outputPath}");

        return outputPath;
    }
}