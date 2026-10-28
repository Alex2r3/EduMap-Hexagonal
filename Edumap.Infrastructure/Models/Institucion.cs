using System;
using System.Collections.Generic;

namespace Edumap.Infrastructure.Models;

/// <summary>
/// Directorio de instituciones educativas con ubicacion y contacto
/// </summary>
public partial class Institucion
{
    public int IdInstitucion { get; set; }

    public int IdCategoria { get; set; }

    public int IdDistrito { get; set; }

    /// <summary>
    /// Usuario con rol REPRESENTANTE que administra la institucion (opcional)
    /// </summary>
    public int? IdRepresentante { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoGestion { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Direccion { get; set; } = null!;

    public double? Latitud { get; set; }

    public double? Longitud { get; set; }

    public string? Telefono { get; set; }

    public string? Whatsapp { get; set; }

    public string? Email { get; set; }

    public string? SitioWeb { get; set; }

    public string? LogoUrl { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    public virtual Distrito IdDistritoNavigation { get; set; } = null!;

    public virtual Usuario? IdRepresentanteNavigation { get; set; }

    public virtual ICollection<OfertaEducativa> OfertaEducativas { get; set; } = new List<OfertaEducativa>();

    public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
