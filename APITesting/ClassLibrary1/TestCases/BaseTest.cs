using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using RestSharp;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

public class TestBase
{
    protected RestClient Client;
    protected string BaseUrl;
    protected string Token;

    protected static string GlobalIsbn;
    protected static string CreatedBookId;

    protected static ExtentReports extent;
    protected static ExtentTest test;

    [OneTimeSetUp]
    public void Setup()
    {
        var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("appsettings.json"));
        BaseUrl = config["BaseUrl"];
        Token = config["Token"];

        Client = new RestClient(BaseUrl);
        Client.AddDefaultHeader("Authorization", $"Bearer {Token}");

        GlobalIsbn = $"978-{new Random().Next(100000000, 999999999)}";
        Console.WriteLine($"Generated Global ISBN: {GlobalIsbn}");

        if (extent == null)
        {
            var htmlReporter = new ExtentSparkReporter("ExtentReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }
    }

    [SetUp]
    public void BeforeTest()
    {
        test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        test.Info($"Starting test: {TestContext.CurrentContext.Test.Name}");
    }

    [TearDown]
    public void AfterTest()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        var message = TestContext.CurrentContext.Result.Message;

        switch (status)
        {
            case NUnit.Framework.Interfaces.TestStatus.Passed:
                test.Pass("Test Passed");
                break;
            case NUnit.Framework.Interfaces.TestStatus.Failed:
                test.Fail($"Test Failed: {message}");
                break;
            case NUnit.Framework.Interfaces.TestStatus.Skipped:
                test.Skip("Test Skipped");
                break;
        }
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        extent.Flush();
        Client?.Dispose();
    }
}