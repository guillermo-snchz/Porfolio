using Microsoft.EntityFrameworkCore;
using Porfolio.Web.Data.Models;
using Porfolio.Web.Models;

namespace Porfolio.Web.Data;

public static class SeedData
{
    public static void InitializeData(IServiceProvider serviceProvider)
    {
        using var context = new PortfolioContext(
            serviceProvider.GetRequiredService<DbContextOptions<PortfolioContext>>());

        //run migrations
        context.Database.Migrate();

        if (context.Estudios.Any()) return;

        context.Estudios.AddRange(
            new Estudios
            {
                Titulo = "Grado Ingeniería de software (No finalizado)",
                Ubicacion = "UPM Campus Sur",
                FechaInicio = new DateTime(2015, 9, 1),
                FechaFin = new DateTime(2018, 6, 1)
            },
            new Estudios
            {
                Titulo = "CFGS Desarrollo de aplicaciones Multiplataforma",
                Ubicacion = "IES Alonso de Avellaneda",
                FechaInicio = new DateTime(2019, 9, 1),
                FechaFin = new DateTime(2021, 6, 1)
            }
        );

        context.Experiencias.AddRange(
            new Experiencia
            {
                Empresa = "CIBERNOS S.A.",
                Ubicacion = "Madrid, España",
                Puesto = "Becario desarrollo",
                FechaInicio = new DateTime(2018, 10, 1),
                FechaFin = new DateTime(2019, 9, 1),
                Funciones = new List<FuncionTrabajo>
                    {
                        new FuncionTrabajo { Descripcion = "Desarrollo a través de herramienta interna LowCode para la creación de aplicaciones web." }
                    }
            },
                new Experiencia
                {
                    Empresa = "CIBERNOS S.A.",
                    Ubicacion = "Madrid, España",
                    Puesto = "Becario desarrollo",
                    FechaInicio = new DateTime(2021, 5, 1),
                    FechaFin = new DateTime(2021, 12, 1),
                    Funciones = new List<FuncionTrabajo>
                    {
                        new FuncionTrabajo { Descripcion = "Desarrollo a través de herramienta interna LowCode para la creación de aplicaciones web." }
                    }
                },
                new Experiencia
                {
                    Empresa = "Gestyde S.L.",
                    Ubicacion = null, // opcional
                    Puesto = "FullStack Developer",
                    FechaInicio = new DateTime(2021, 12, 1),
                    FechaFin = new DateTime(2025, 3, 1),
                    Funciones = new List<FuncionTrabajo>
                    {
                        new FuncionTrabajo { Descripcion = "Desarrollador principal de una aplicación web basada en microservicios utilizando .NET 6 con múltiples bases de datos SQL Server. Participación en todo el ciclo de desarrollo de nuevas funcionalidades, resolución de problemas y mejora de servicios existentes aplicando buenas prácticas para lograr una mayor mantenibilidad." },
                        new FuncionTrabajo { Descripcion = "Tareas de mantenimiento y desarrollo en aplicaciones web monolíticas con .NET Framework 4.8." },
                        new FuncionTrabajo { Descripcion = "Integraciones con servicios externos en procesos existentes que necesitaban ser mejorados o ajustarse a normativas legales." }
                    }
                }
            );

        context.StackTecnologico.AddRange(
            new Stack { Nombre = "C#", Nivel = 4 },
                new Stack { Nombre = "ASP.NET Core", Nivel = 4 },
                new Stack { Nombre = "Azure", Nivel = 1 },
                new Stack { Nombre = "SQL Server", Nivel = 4 },
                new Stack { Nombre = "JavaScript", Nivel = 3 },
                new Stack { Nombre = "Git", Nivel = 3 },
                new Stack { Nombre = "Docker", Nivel = 1 }
                );

        context.SaveChanges();
    }
}
