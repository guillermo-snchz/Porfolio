using Microsoft.AspNetCore.Mvc;

namespace Porfolio.Web.Controllers;

public class ProjectsController : Controller
{
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(ILogger<ProjectsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
