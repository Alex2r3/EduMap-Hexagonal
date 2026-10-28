using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

/// <summary>
/// Programas o niveles que ofrece cada institucion, con pension estimada
/// </summary>
public partial class OfertaEducativa
{
    public int IdOferta { get; set; }

    public int IdInstitucion { get; set; }

    public int IdNivel { get; set; }

    public string Nombre { get; set; } = null!;

    public string Modalidad { get; set; } = null!;

    public string? Descripcion { get; set; }

    public short? DuracionMeses { get; set; }

    public decimal? PensionMin { get; set; }

    public decimal? PensionMax { get; set; }

    public decimal? Matricula { get; set; }

    public bool Activa { get; set; }

    public virtual Institucion IdInstitucionNavigation { get; set; } = null!;

    public virtual NivelEducativo IdNivelNavigation { get; set; } = null!;
}
