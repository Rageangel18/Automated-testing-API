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
        var request = new RestRequest("/Books", Method.Get);
        var response = await Client.ExecuteAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var books = JsonConvert.DeserializeObject<List<dynamic>>(response.Content);
        books.Should().NotBeNullOrEmpty();
    }
}