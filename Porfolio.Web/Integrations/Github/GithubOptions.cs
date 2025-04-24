namespace Porfolio.Web.Integrations.Github;

public class GithubOptions
{
    public string Username { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://api.github.com";
    public string? Token { get; set; }
    public int MaxRepos { get; set; } = 6;

}
