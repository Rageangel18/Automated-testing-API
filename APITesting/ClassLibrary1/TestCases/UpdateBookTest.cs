using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

public class UpdateBookTests : TestBase
{
    [Test, Order(4)]
    public async Task UpdateBook_ShouldReturn200()
    {
        var request = new RestRequest($"/Books/ba1b5f13-7043-4d83-aad9-2742d9868742", Method.Put);

        request.AddJsonBody(new
        {
            title = "Updated Title",
            author = "string",
            publishedDate = "2025-01-16T15:29:43.342Z"
        });

        var response = await Client.ExecuteAsync(request);

        Console.WriteLine($"Response Status Code: {response.StatusCode}");
        Console.WriteLine($"Response Content: {response.Content}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}