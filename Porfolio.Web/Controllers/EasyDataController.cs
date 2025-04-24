using Microsoft.AspNetCore.Mvc;

namespace Porfolio.Web.Controllers;

[Route("easydata")]
public class EasyDataController : Controller
{
    private readonly ILogger<EasyDataController> _logger;

    public EasyDataController(ILogger<EasyDataController> logger)
    {
        _logger = logger;
    }

    [Route(template: "{**entity}")]
    public IActionResult Index(string entity)
    {
        return View();
    }
}
