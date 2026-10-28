using MediatR;

namespace Edumap.Application.UseCases.Resenas.Queries;

// Query CQRS: Consulta la lista de reseñas asociadas a una institución específica
public record GetResenasByInstitucionIdQuery(Guid InstitucionId) : IRequest<IEnumerable<ResenaDto>>;

public record ResenaDto(Guid ResenaId, string Usuario, int Calificacion, string Comentario, DateTime Fecha);

internal sealed class GetResenasByInstitucionIdQueryHandler : IRequestHandler<GetResenasByInstitucionIdQuery, IEnumerable<ResenaDto>>
{
    public async Task<IEnumerable<ResenaDto>> Handle(GetResenasByInstitucionIdQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Enumerable.Empty<ResenaDto>();
    }
}
