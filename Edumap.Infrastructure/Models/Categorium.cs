using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

/// <summary>
/// Tipo de institucion: colegio, academia, instituto, universidad
/// </summary>
public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string? IconoUrl { get; set; }

    public virtual ICollection<Institucion> Institucions { get; set; } = new List<Institucion>();
}
