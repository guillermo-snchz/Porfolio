using EasyData.EntityFrameworkCore;

namespace Porfolio.Web.Data.Models;

[MetaEntity(DisplayName = "Estudios")]
public class Estudios
{
    public int Id { get; set; } // Id de la base de datos
    public string? Titulo { get; set; }
    public string? Ubicacion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
}

public class Experiencia
{
    public int Id { get; set; } // Id de la base de datos
    public string? Empresa { get; set; }
    public string? Ubicacion { get; set; }
    public string? Puesto { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public ICollection<FuncionTrabajo>? Funciones { get; set; }
}

[MetaEntity(DisplayName = "Funciones Trabajo")]

public class FuncionTrabajo
{
    public int Id { get; set; } // Id de la base de datos
    public string? Descripcion { get; set; }
    public int ExperienciaId { get; set; } // Id de la experiencia a la que pertenece
    public Experiencia? Experiencia { get; set; } // Navegación a la experiencia
}

[MetaEntity(DisplayName = "Stack Tecnologico")]
public class Stack
{
    public int Id { get; set; } // Id de la base de datos
    public string? Nombre { get; set; }
    public int Nivel { get; set; } // 1-5
    public string? Icono { get; set; }
    public string? Url { get; set; }
}
