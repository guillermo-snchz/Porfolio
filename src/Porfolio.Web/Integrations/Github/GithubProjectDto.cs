using System.Text.Json.Serialization;

namespace Porfolio.Web.Integrations.Github;

public class GithubProjectDto
{
    public string? Name { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }
    
    public string? Url { get; set; }

    public string? Description { get; set; }
    
    public string[]? Languages { get; set; }

    [JsonPropertyName("private")]
    public bool IsPrivate { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}