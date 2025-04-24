using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Porfolio.Web.Models;
using Porfolio.Web.Services.Email;

namespace Porfolio.Web.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;
    private readonly IEmailService _emailService;

    public ContactController(ILogger<ContactController> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new ContactFormViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(ContactFormViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);

        try
        {
            await _emailService.SendEmailAsync(model.Nombre, model.Email, model.Mensaje);
            TempData["SuccessMessage"] = "¡Tu mensaje fue enviado con éxito!";
        }
        catch
        {
            TempData["ErrorMessage"] = "Hubo un error al enviar el mensaje.";
        }

        return RedirectToAction("Index");
    }
}