using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

/// <summary>
/// Niveles: inicial, primaria, secundaria, pregrado, etc.
/// </summary>
public partial class NivelEducativo
{
    public int IdNivel { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<OfertaEducativa> OfertaEducativas { get; set; } = new List<OfertaEducativa>();
}
