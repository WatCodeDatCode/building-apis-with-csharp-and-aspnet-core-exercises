using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using TheEmployeeAPI.Employees;

namespace TheEmployeeAPI.Tests;

public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory) 
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAllEmployees_ReturnsOkResult()
    {
        HttpClient client = _factory.CreateClient();
        var response = await client.GetAsync("/employees");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetEmployeeById_ReturnsOkResult()
    {
        HttpClient client = _factory.CreateClient();
        var response = await client.GetAsync("/employees/1");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateEmployee_ReturnsOkResult()
    {
        HttpClient client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/employees", new Employee { 
            FirstName = "John", LastName = "Doe", SocialSecurityNumber = "123-55-2123" });

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateEmployee_ReturnsBadRequestResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var invalidEmployee = new CreateEmployeeRequest(); // Empty object to trigger validation

        // Act
        var response = await client.PostAsJsonAsync("/employees", invalidEmployee);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(problemDetails);
        Assert.Contains("FirstName", problemDetails.Errors.Keys);
        Assert.Contains("LastName", problemDetails.Errors.Keys);
        Assert.Contains("The FirstName field is required.", problemDetails.Errors["FirstName"]);
        Assert.Contains("The LastName field is required.", problemDetails.Errors["LastName"]);
    }

    [Fact]
    public async Task UpdateEmployee_ReturnsOkResult()
    {
        var client = _factory.CreateClient();
        var response = await client.PutAsJsonAsync("/employees/1", new Employee {
            FirstName = "Bob", LastName = "Brown", SocialSecurityNumber = "124-12-4124"});

            response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task UpdateEmployee_ReturnsNotFoundResult()
    {
        var client = _factory.CreateClient();
        var response = await client.PutAsJsonAsync("/employees/09999", new Employee {
            FirstName = "Bob", LastName = "Brown", SocialSecurityNumber = "124-12-4124"});

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}