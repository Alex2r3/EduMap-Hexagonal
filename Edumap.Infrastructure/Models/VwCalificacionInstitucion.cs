using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

public partial class VwCalificacionInstitucion
{
    public int? IdInstitucion { get; set; }

    public decimal? PromedioCalificacion { get; set; }

    public long? TotalResenas { get; set; }
}
