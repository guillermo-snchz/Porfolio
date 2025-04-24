using Microsoft.EntityFrameworkCore;
using Porfolio.Web.Data.Models;

namespace Porfolio.Web.Repository;

public class AboutRepository : IAboutRepository
{
    private readonly PortfolioContext _context;

    public AboutRepository(PortfolioContext context)
    {
        _context = context;
    }

    public async Task<List<Estudios>> GetEstudiosAsync()
    {
        return await _context.Estudios
            .OrderByDescending(e => e.FechaInicio)
            .ToListAsync();
    }

    public async Task<List<Experiencia>> GetExperienciasAsync()
    {
        return await _context.Experiencias
            .Include(e => e.Funciones)
            .OrderByDescending(e => e.FechaInicio)
            .ToListAsync();
    }

    public async Task<List<Stack>> GetStackTecnologicoAsync()
    {
        return await _context.StackTecnologico
            .OrderByDescending(s => s.Nivel)
            .ToListAsync();
    }
}

