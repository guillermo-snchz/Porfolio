using Microsoft.AspNetCore.Mvc;
using Porfolio.Web.Integrations.Github;
using Porfolio.Web.Models;

namespace Porfolio.Web.Controllers;

public class ProjectsController : Controller
{
    private readonly ILogger<ProjectsController> _logger;
    private readonly IGithubService _githubService;

    public ProjectsController(ILogger<ProjectsController> logger, IGithubService githubService)
    {
        _logger = logger;
        _githubService = githubService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _githubService.GetPublicRepositoriesAsync();

        var viewModel = new ProjectsViewModel
        {
            Projects = projects
        };

        return View(viewModel);
    }
}