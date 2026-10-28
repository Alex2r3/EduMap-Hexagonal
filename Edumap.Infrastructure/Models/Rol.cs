using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

/// <summary>
/// Roles de acceso: USUARIO, REPRESENTANTE, ADMINISTRADOR
/// </summary>
public partial class Rol
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
