using Microsoft.EntityFrameworkCore;
using Porfolio.Web.Data.Models;

public class PortfolioContext : DbContext
{
    public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) { }

    public DbSet<Estudios> Estudios { get; set; }
    public DbSet<Experiencia> Experiencias { get; set; }
    public DbSet<FuncionTrabajo> FuncionesTrabajo { get; set; }
    public DbSet<Stack> StackTecnologico { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación Experiencia 1 - n Funciones
        modelBuilder.Entity<Experiencia>()
            .HasMany(e => e.Funciones)
            .WithOne(f => f.Experiencia)
            .HasForeignKey(f => f.ExperienciaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
