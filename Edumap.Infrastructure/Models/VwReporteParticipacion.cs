using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

public partial class VwReporteParticipacion
{
    public long? TotalUsuarios { get; set; }

    public long? UsuariosActivos { get; set; }

    public long? TotalResenas { get; set; }

    public long? UsuariosConResenas { get; set; }
}
