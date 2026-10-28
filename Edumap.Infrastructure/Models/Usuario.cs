using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

/// <summary>
/// Cuentas del sistema con su rol
/// </summary>
public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? FotoUrl { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Institucion> Institucions { get; set; } = new List<Institucion>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
