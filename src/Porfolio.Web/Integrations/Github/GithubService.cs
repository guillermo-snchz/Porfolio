using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Porfolio.Web.Core;
using Porfolio.Web.Services.MemoryCache;

namespace Porfolio.Web.Integrations.Github;

public class GithubService : IGithubService
{
    private readonly HttpClient _httpClient;
    private readonly GithubOptions _options;

    private readonly IApiCacheService _apiCacheService;

    public GithubService(HttpClient httpClient, IOptions<GithubOptions> options, IApiCacheService apiCacheService)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("request"); // GitHub lo requiere
        if (!string.IsNullOrEmpty(_options.Token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", _options.Token);
        }

        _apiCacheService = apiCacheService;
    }

    public async Task<IEnumerable<GithubProjectDto>?> GetPublicRepositoriesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"/users/{_options.Username}/repos?sort=updated");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var repos = JsonSerializer.Deserialize<List<GithubProjectDto>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (repos is null)
            {
                return null;
            }

            await Parallel.ForEachAsync(repos, async (repo, cancellationToken) =>
            {
                var languages = await GetLanguagesFromRepositoryAsync(repo.Name!, cancellationToken);
                repo.Languages = [.. languages];
            });

            _apiCacheService.Set("GithubProjects", repos, TimeSpan.FromHours(1));

            return repos.Take(_options.MaxRepos);
        }
        catch
        {
            return null;
        }
    }

    private async Task<IEnumerable<string>> GetLanguagesFromRepositoryAsync(string repoName, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/repos/{_options.Username}/{repoName}/languages", cancellationToken);

            response.EnsureSuccessStatusCode();

            string? content = await response.Content.ReadAsStringAsync(cancellationToken);

            Dictionary<string, int>? languages = JsonSerializer.Deserialize<Dictionary<string, int>>(
                content,
                Tools.GetJsonSerializerOptions());

            return languages?.Keys.ToArray() ?? [];
        }
        catch
        {
            return [];
        }
    }


}