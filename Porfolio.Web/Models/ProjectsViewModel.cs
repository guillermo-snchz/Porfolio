using Porfolio.Web.Integrations.Github;

namespace Porfolio.Web.Models;

public class ProjectsViewModel
{
    public IEnumerable<GithubProjectDto>? Projects { get; set; }
}
