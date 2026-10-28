using MediatR;

namespace Edumap.Application.UseCases.Instituciones.Queries;

// Query CQRS: Consulta los datos detallados de una institución educativa por su identificador
public record GetInstitucionByIdQuery(Guid InstitucionId) : IRequest<InstitucionDetailDto?>;

public record InstitucionDetailDto(Guid InstitucionId, string Nombre, string Direccion, string Telefono, string Distrito);

internal sealed class GetInstitucionByIdQueryHandler : IRequestHandler<GetInstitucionByIdQuery, InstitucionDetailDto?>
{
    public async Task<InstitucionDetailDto?> Handle(GetInstitucionByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Consultar repositorio a través de los puertos
        await Task.CompletedTask;
        return null;
    }
}
