using Edumap.Domain.Common;
using Edumap.Domain.Exceptions;

namespace Edumap.Domain.Entities;

// Raíz de agregado: institución educativa del directorio, con su ubicación y datos de contacto
public class Institucion : IAggregateRoot
{
    public const string EstadoPendiente = "PENDIENTE";

    private Institucion() { }

    public int IdInstitucion { get; private set; }

    public int IdCategoria { get; private set; }

    public int IdDistrito { get; private set; }

    // Usuario con rol REPRESENTANTE que administra la institución (opcional)
    public int? IdRepresentante { get; private set; }

    public string Nombre { get; private set; } = null!;

    public string TipoGestion { get; private set; } = null!;

    public string? Descripcion { get; private set; }

    public string Direccion { get; private set; } = null!;

    public double? Latitud { get; private set; }

    public double? Longitud { get; private set; }

    public string? Telefono { get; private set; }

    public string? Whatsapp { get; private set; }

    public string? Email { get; private set; }

    public string? SitioWeb { get; private set; }

    public string? LogoUrl { get; private set; }

    public string Estado { get; private set; } = null!;

    public DateTime FechaRegistro { get; private set; }

    public DateTime FechaActualizacion { get; private set; }

    public Categoria Categoria { get; private set; } = null!;

    public Distrito Distrito { get; private set; } = null!;

    public Usuario? Representante { get; private set; }

    public ICollection<OfertaEducativa> OfertasEducativas { get; private set; } = new List<OfertaEducativa>();

    public ICollection<Resena> Resenas { get; private set; } = new List<Resena>();

    // Toda institución nueva queda PENDIENTE de revisión; las fechas las asigna la base de datos (DEFAULT now())
    public static Institucion Crear(
        int idCategoria,
        int idDistrito,
        string nombre,
        string tipoGestion,
        string direccion,
        string? descripcion,
        double? latitud,
        double? longitud,
        string? telefono,
        string? whatsapp,
        string? email,
        string? sitioWeb)
    {
        if (latitud.HasValue != longitud.HasValue)
            throw new DomainException("La latitud y la longitud deben indicarse juntas.");
        if (latitud is < -90 or > 90)
            throw new DomainException("La latitud debe estar entre -90 y 90.");
        if (longitud is < -180 or > 180)
            throw new DomainException("La longitud debe estar entre -180 y 180.");

        return new Institucion
        {
            IdCategoria = idCategoria,
            IdDistrito = idDistrito,
            Nombre = Guard.Required(nombre, "nombre", 150),
            TipoGestion = Guard.Required(tipoGestion, "tipoGestion", 10).ToUpperInvariant(),
            Direccion = Guard.Required(direccion, "direccion", 200),
            Descripcion = Guard.Optional(descripcion, "descripcion"),
            Latitud = latitud,
            Longitud = longitud,
            Telefono = Guard.Optional(telefono, "telefono", 20),
            Whatsapp = Guard.Optional(whatsapp, "whatsapp", 20),
            Email = Guard.Optional(email, "email", 120),
            SitioWeb = Guard.Optional(sitioWeb, "sitioWeb", 200),
            Estado = EstadoPendiente
        };
    }
}
