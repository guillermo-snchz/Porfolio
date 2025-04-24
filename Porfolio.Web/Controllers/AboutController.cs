using Microsoft.AspNetCore.Mvc;
using Porfolio.Web.Data.Models;
using Porfolio.Web.Models;
using Porfolio.Web.Repository;

namespace Porfolio.Web.Controllers;

public class AboutController : Controller
{
    private readonly ILogger<AboutController> _logger;

    private readonly IAboutRepository _aboutRepository;

    public AboutController(ILogger<AboutController> logger, IAboutRepository aboutRepository)
    {
        _logger = logger;
        _aboutRepository = aboutRepository;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Estudios> estudios = await _aboutRepository.GetEstudiosAsync();
        var experiencias = await _aboutRepository.GetExperienciasAsync();
        var stackTecnologico = await _aboutRepository.GetStackTecnologicoAsync();

        AboutViewModel viewModel = new()
        {
            Experiencia = experiencias,
            StackTecnologico = stackTecnologico,
            Estudios = estudios
        };

        return View(viewModel);
    }
}
