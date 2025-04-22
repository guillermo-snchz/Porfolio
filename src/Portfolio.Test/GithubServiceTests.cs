using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Porfolio.Web.Integrations.Github;

namespace Portfolio.Test;

public class GithubServiceTests
{
    [Fact]
    public async Task GetPublicRepositoriesAsync_ReturnsProjects()
    {
        // Arrange
        var fakeJson = JsonSerializer.Serialize(new[]
        {
            new {
                name = "MiProyecto",
                description = "Descripción",
                html_url = "https://github.com/user/MiProyecto",
                url = "https://api.github.com/repos/fake-user/MiProyecto",
                languages = new List<string>(){"C#"}.ToArray(),
                isPrivate = false,
            }
        });

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeJson),
            });

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.github.com")
        };

        var options = Options.Create(new GithubOptions
        {
            Username = "fake-user"
        });

        var service = new GithubService(httpClient, options);

        // Act
        var result = await service.GetPublicRepositoriesAsync();

        // Assert
        var project = Assert.Single(result);
        Assert.Equal("MiProyecto", project.Name);
        Assert.Equal("Descripción", project.Description);
        Assert.Equal("https://github.com/user/MiProyecto", project.HtmlUrl);
        Assert.Equal("https://api.github.com/repos/fake-user/MiProyecto", project.Url);
        Assert.Equal(["C#"], project.Languages!);
        Assert.False(project.IsPrivate);
    }

    [Fact]
    public async Task GetPublicRepositoriesAsync_RealCall_Works()
    {
        var options = Options.Create(new GithubOptions
        {
            Username = "guillermo-snchz", // Cambia por un usuario real
            BaseUrl = "https://api.github.com"
        });

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.Value.BaseUrl)
        };

        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("TestClient");

        var service = new GithubService(httpClient, options);

        var repos = await service.GetPublicRepositoriesAsync();

        Assert.NotEmpty(repos);
    }
}