using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

public class GetBookByIdTests : TestBase
{
    [Test, Order(3)]
    public async Task GetBookById_ShouldReturnCorrectBook()
    {
        test.Info("Starting test: GetBookById_ShouldReturnCorrectBook");

        var request = new RestRequest("/Books/d5b7c827-04db-4ecf-b6d6-bdeed470e503", Method.Get);
        var response = await Client.ExecuteAsync(request);

        test.Info($"Response Status Code: {response.StatusCode}");
        test.Info($"Response Content: {response.Content}");

        try
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var jsonResponse = JObject.Parse(response.Content);
            jsonResponse["id"]!.ToString().Should().Be("d5b7c827-04db-4ecf-b6d6-bdeed470e503");
            test.Pass("Book retrieved successfully.");
        }
        catch (Exception ex)
        {
            test.Fail($"Test Failed: {ex.Message}");
            throw;
        }
    }
}