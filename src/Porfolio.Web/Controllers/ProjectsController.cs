using Microsoft.AspNetCore.Mvc;
using Porfolio.Web.Integrations.Github;
using Porfolio.Web.Models;
using Porfolio.Web.Services.MemoryCache;

namespace Porfolio.Web.Controllers;

public class ProjectsController : Controller
{
    private readonly ILogger<ProjectsController> _logger;

    private readonly IApiCacheService _apiCacheService;
    private readonly IGithubService _githubService;

    public ProjectsController(ILogger<ProjectsController> logger, IGithubService githubService, IApiCacheService apiCacheService)
    {
        _logger = logger;
        _githubService = githubService;
        _apiCacheService = apiCacheService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = _apiCacheService.Get<IEnumerable<GithubProjectDto>>("GithubProjects");

        if (projects is not null)
        {
            return View(new ProjectsViewModel { Projects = projects });
        }

        projects = await _githubService.GetPublicRepositoriesAsync();

        var viewModel = new ProjectsViewModel
        {
            Projects = projects
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> ReloadFromGithub()
    {
        var projects = await _githubService.GetPublicRepositoriesAsync();

        if (projects is not null)
        {
            _apiCacheService.Set("GithubProjects", projects, TimeSpan.FromHours(1));
        }
        return RedirectToAction("Index");
    }
}