﻿using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

public class CreateBookTests : TestBase
{
    [Test, Order(1)]
    public async Task CreateBook_ShouldReturn201()
    {
        test.Info("Starting test: CreateBook_ShouldReturn201");

        var request = new RestRequest("/Books", Method.Post);
        request.AddJsonBody(new
        {
            title = "Test name",
            author = "Nikita Woki - legenda",
            isbn = GlobalIsbn,
            publishedDate = "2004-12-12T00:00:00Z"
        });

        test.Info("Sending request to create book...");

        var response = await Client.ExecuteAsync(request);

        test.Info($"Response Status Code: {response.StatusCode}");
        test.Info($"Response Content: {response.Content}");

        try
        {
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var jsonResponse = JObject.Parse(response.Content);

            jsonResponse["title"]!.ToString().Should().Be("Test name");
            jsonResponse["author"]!.ToString().Should().Be("Nikita Woki - legenda");
            jsonResponse["isbn"]!.ToString().Should().Be(GlobalIsbn);

            CreatedBookId = jsonResponse["id"]?.ToString();
            CreatedBookId.Should().NotBeNullOrEmpty();

            test.Pass($"Book created successfully with ID: {CreatedBookId}");
        }
        catch (Exception ex)
        {
            test.Fail($"Test Failed: {ex.Message}");
            throw;
        }
    }
}