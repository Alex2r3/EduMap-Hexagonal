using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

/// <summary>
/// Calificacion (1-5) y comentario; una por usuario e institucion
/// </summary>
public partial class Resena
{
    public int IdResena { get; set; }

    public int IdUsuario { get; set; }

    public int IdInstitucion { get; set; }

    public short Calificacion { get; set; }

    public string? Comentario { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Institucion IdInstitucionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
