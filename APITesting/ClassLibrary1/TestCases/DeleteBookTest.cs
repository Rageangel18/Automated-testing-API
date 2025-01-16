using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

public class DeleteBookTests : TestBase
{
    [Test, Order(5)]
    public async Task DeleteBook_ShouldReturn204()
    {
        test.Info("Starting test: DeleteBook_ShouldReturn204");

        var bookId = CreatedBookId;

        test.Info($"Attempting to delete book with ID: {bookId}");

        var request = new RestRequest($"/Books/{bookId}", Method.Delete);
        var response = await Client.ExecuteAsync(request);

        test.Info($"Response Status Code: {response.StatusCode}");

        try
        {
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            test.Pass($"Book with ID {bookId} deleted successfully.");
        }
        catch (Exception ex)
        {
            test.Fail($"Test Failed: {ex.Message}");
            throw;
        }
        test.Info($"Verifying that book with ID {bookId} no longer exists...");

        var checkRequest = new RestRequest($"/Books/{bookId}", Method.Get);
        var checkResponse = await Client.ExecuteAsync(checkRequest);

        test.Info($"Verification Response Status Code: {checkResponse.StatusCode}");

        try
        {
            checkResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
            test.Pass($"Book with ID {bookId} was successfully removed.");
        }
        catch (Exception ex)
        {
            test.Fail($"Verification Failed: {ex.Message}");
            throw;
        }
    }
}