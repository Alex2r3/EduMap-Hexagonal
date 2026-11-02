using System.Net.Mail;
using Edumap.Domain.Common;
using Edumap.Domain.Exceptions;

namespace Edumap.Domain.Entities;

// Raíz de agregado: cuenta del sistema con su rol de acceso
public class Usuario : IAggregateRoot
{
    public const string EstadoActivo = "ACTIVO";

    private Usuario() { }

    public int IdUsuario { get; private set; }

    public int IdRol { get; private set; }

    public string Nombres { get; private set; } = null!;

    public string Apellidos { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public string? Telefono { get; private set; }

    public string? FotoUrl { get; private set; }

    public string Estado { get; private set; } = null!;

    public DateTime FechaRegistro { get; private set; }

    public DateTime? UltimoAcceso { get; private set; }

    public Rol Rol { get; private set; } = null!;

    public ICollection<Institucion> InstitucionesRepresentadas { get; private set; } = new List<Institucion>();

    public ICollection<Resena> Resenas { get; private set; } = new List<Resena>();

    // Recibe la clave ya hasheada: el dominio nunca conoce la contraseña en texto plano
    public static Usuario Registrar(int idRol, string nombres, string apellidos, string email, string passwordHash, string? telefono)
    {
        return new Usuario
        {
            IdRol = idRol,
            Nombres = Guard.Required(nombres, "nombres", 80),
            Apellidos = Guard.Required(apellidos, "apellidos", 80),
            Email = NormalizarEmail(email),
            PasswordHash = Guard.Required(passwordHash, "passwordHash", 255),
            Telefono = Guard.Optional(telefono, "telefono", 20),
            Estado = EstadoActivo
        };
    }

    // El email se guarda en minúsculas para que la verificación de duplicados no dependa de mayúsculas
    public static string NormalizarEmail(string? email)
    {
        var normalizado = Guard.Required(email, "email", 120).ToLowerInvariant();

        if (!MailAddress.TryCreate(normalizado, out var direccion) || direccion.Address != normalizado)
            throw new DomainException("El email no tiene un formato válido.");

        return normalizado;
    }
}
