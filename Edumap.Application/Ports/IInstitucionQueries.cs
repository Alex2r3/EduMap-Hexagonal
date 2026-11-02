using Edumap.Application.UseCases.Instituciones.Queries;

namespace Edumap.Application.Ports;

// Puerto de Lectura (CQRS): consultas de instituciones proyectadas directamente a DTOs
public interface IInstitucionQueries
{
    Task<InstitucionDetailDto?> GetByIdAsync(int idInstitucion, CancellationToken cancellationToken);
}
