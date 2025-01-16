using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using RestSharp;
using NUnit.Framework;

public class TestBase
{
    protected RestClient Client;
    protected string BaseUrl;
    protected string Token;

    protected static string GlobalIsbn;
    protected static string CreatedBookId;


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
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        Client?.Dispose();
    }
}