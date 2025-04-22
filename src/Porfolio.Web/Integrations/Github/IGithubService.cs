namespace Porfolio.Web.Integrations.Github;

public interface IGithubService
{
    Task<IEnumerable<GithubProjectDto>?> GetPublicRepositoriesAsync();
}
