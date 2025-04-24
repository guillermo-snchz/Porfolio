using Porfolio.Web.Data.Models;

namespace Porfolio.Web.Models;

public class AboutViewModel
{
    public IEnumerable<Estudios>? Estudios { get; set; }

    public IEnumerable<Experiencia>? Experiencia { get; set; }

    public IEnumerable<Stack>? StackTecnologico { get; set; }
}