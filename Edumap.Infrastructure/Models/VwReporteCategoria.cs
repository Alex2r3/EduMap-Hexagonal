using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

public partial class VwReporteCategoria
{
    public string? Categoria { get; set; }

    public long? TotalInstituciones { get; set; }

    public decimal? CalificacionPromedio { get; set; }
}
