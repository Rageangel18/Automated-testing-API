using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

public class GetBookByIdTests : TestBase
{
    [Test, Order(3)]
    public async Task GetBookById_ShouldReturnCorrectBook()
    {

        var request = new RestRequest("/Books/d5b7c827-04db-4ecf-b6d6-bdeed470e503", Method.Get);

        var response = await Client.ExecuteAsync(request);

        Console.WriteLine($"Response Status Code: {response.StatusCode}");
        Console.WriteLine($"Response Content: {response.Content}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var jsonResponse = JObject.Parse(response.Content);

        jsonResponse["id"]!.ToString().Should().Be("d5b7c827-04db-4ecf-b6d6-bdeed470e503");
    }
}