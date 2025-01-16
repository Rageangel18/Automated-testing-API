using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

public class DeleteBookTests : TestBase
{
    [Test, Order(5)]
    public async Task DeleteBook_ShouldReturn204()
    {
        var bookId = CreatedBookId;
        var request = new RestRequest($"/Books/{bookId}", Method.Delete);
        var response = await Client.ExecuteAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var checkRequest = new RestRequest($"/Books/{bookId}", Method.Get);
        var checkResponse = await Client.ExecuteAsync(checkRequest);
        checkResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}