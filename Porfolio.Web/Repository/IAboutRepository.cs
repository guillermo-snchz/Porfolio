using Porfolio.Web.Data.Models;

namespace Porfolio.Web.Repository;

public interface IAboutRepository
{
    Task<List<Estudios>> GetEstudiosAsync();
    Task<List<Experiencia>> GetExperienciasAsync();
    Task<List<Stack>> GetStackTecnologicoAsync();
}
