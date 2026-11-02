using Edumap.Domain.Common;
using Edumap.Domain.Exceptions;

namespace Edumap.Domain.Entities;

// Raíz de agregado: calificación (1-5) y comentario de un usuario sobre una institución; una por usuario e institución
public class Resena : IAggregateRoot
{
    public const string EstadoVisible = "VISIBLE";
    public const short CalificacionMinima = 1;
    public const short CalificacionMaxima = 5;

    private Resena() { }

    public int IdResena { get; private set; }

    public int IdUsuario { get; private set; }

    public int IdInstitucion { get; private set; }

    public short Calificacion { get; private set; }

    public string? Comentario { get; private set; }

    public string Estado { get; private set; } = null!;

    public DateTime FechaCreacion { get; private set; }

    public DateTime? FechaActualizacion { get; private set; }

    public Institucion Institucion { get; private set; } = null!;

    public Usuario Usuario { get; private set; } = null!;

    public static Resena Crear(int idInstitucion, int idUsuario, short calificacion, string? comentario)
    {
        if (calificacion is < CalificacionMinima or > CalificacionMaxima)
            throw new DomainException($"La calificación debe estar entre {CalificacionMinima} y {CalificacionMaxima}.");

        return new Resena
        {
            IdInstitucion = idInstitucion,
            IdUsuario = idUsuario,
            Calificacion = calificacion,
            Comentario = Guard.Optional(comentario, "comentario"),
            Estado = EstadoVisible
        };
    }
}
