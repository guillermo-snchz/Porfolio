using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Porfolio.Web.Integrations.Github;

public class GithubService : IGithubService
{
    private readonly HttpClient _httpClient;
    private readonly GithubOptions _options;

    public GithubService(HttpClient httpClient, IOptions<GithubOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("request"); // GitHub lo requiere
        if (!string.IsNullOrEmpty(_options.Token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", _options.Token);
        }
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

            if (repos is not null)
            {
                await Parallel.ForEachAsync(repos, async (repo, cancellationToken) =>
                {
                    var languages = await GetLanguagesFromRepositoryAsync(repo.Name!, cancellationToken);
                    repo.Languages = languages.ToArray();
                });

                return repos.Take(_options.MaxRepos);
            }

            return repos?.Take(_options.MaxRepos) ?? [];
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

            var content = await response.Content.ReadAsStringAsync();

            var languages = JsonSerializer.Deserialize<Dictionary<string, int>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return languages?.Keys.ToArray() ?? [];
        }
        catch
        {
            return [];
        }
    }
}