using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

public class GetAllBooksTests : TestBase
{
    [Test, Order(2)]
    public async Task GetAllBooks_ShouldReturnList()
    {
        test.Info("Starting test: GetAllBooks_ShouldReturnList");

        var request = new RestRequest("/Books", Method.Get);
        var response = await Client.ExecuteAsync(request);

        test.Info($"Response Status Code: {response.StatusCode}");
        test.Info($"Response Content: {response.Content}");

        try
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var books = JsonConvert.DeserializeObject<List<dynamic>>(response.Content);
            books.Should().NotBeNullOrEmpty();
            test.Pass("Book list retrieved successfully.");
        }
        catch (Exception ex)
        {
            test.Fail($"Test Failed: {ex.Message}");
            throw;
        }
    }
}