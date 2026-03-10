using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net.Http;

namespace MicroondasBennerAPI.Tests.Controllers;

public class MicroondasControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public MicroondasControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetStatus_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/microondas/status");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("status");
    }
}