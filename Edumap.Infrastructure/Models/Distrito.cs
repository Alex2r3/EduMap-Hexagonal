using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

/// <summary>
/// Ubicacion geografica para filtros
/// </summary>
public partial class Distrito
{
    public int IdDistrito { get; set; }

    public string Nombre { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public virtual ICollection<Institucion> Institucions { get; set; } = new List<Institucion>();
}
