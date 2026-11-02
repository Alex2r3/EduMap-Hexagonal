using MediatR;
using Edumap.Application.Ports;

namespace Edumap.Application.UseCases.Resenas.Queries;

// Query CQRS: Consulta la lista de reseñas visibles asociadas a una institución específica
public record GetResenasByInstitucionIdQuery(int IdInstitucion) : IRequest<IReadOnlyList<ResenaDto>>;

public record ResenaDto(int IdResena, string Usuario, short Calificacion, string? Comentario, DateTime FechaCreacion);

internal sealed class GetResenasByInstitucionIdQueryHandler(IResenaQueries queries) : IRequestHandler<GetResenasByInstitucionIdQuery, IReadOnlyList<ResenaDto>>
{
    public Task<IReadOnlyList<ResenaDto>> Handle(GetResenasByInstitucionIdQuery request, CancellationToken cancellationToken)
        => queries.GetVisiblesByInstitucionAsync(request.IdInstitucion, cancellationToken);
}
