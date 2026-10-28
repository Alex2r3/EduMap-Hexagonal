using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

public partial class VwInstitucionResuman
{
    public int? IdInstitucion { get; set; }

    public string? Nombre { get; set; }

    public string? TipoGestion { get; set; }

    public string? Direccion { get; set; }

    public double? Latitud { get; set; }

    public double? Longitud { get; set; }

    public string? Estado { get; set; }

    public int? IdCategoria { get; set; }

    public string? Categoria { get; set; }

    public int? IdDistrito { get; set; }

    public string? Distrito { get; set; }

    public string? Provincia { get; set; }

    public decimal? PromedioCalificacion { get; set; }

    public long? TotalResenas { get; set; }

    public decimal? PensionMin { get; set; }

    public decimal? PensionMax { get; set; }
}
