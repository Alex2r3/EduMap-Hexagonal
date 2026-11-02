using Edumap.Application.UseCases.Resenas.Queries;

namespace Edumap.Application.Ports;

// Puerto de Lectura (CQRS): consultas de reseñas proyectadas directamente a DTOs
public interface IResenaQueries
{
    Task<IReadOnlyList<ResenaDto>> GetVisiblesByInstitucionAsync(int idInstitucion, CancellationToken cancellationToken);
}
