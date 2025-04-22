using System.ComponentModel.DataAnnotations;

namespace Porfolio.Web.Models;

public class ContactFormViewModel
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El mensaje es obligatorio")]
    public string Mensaje { get; set; } = string.Empty;
}
