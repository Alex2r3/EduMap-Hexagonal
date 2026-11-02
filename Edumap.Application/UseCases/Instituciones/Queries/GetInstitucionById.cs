using MediatR;
using Edumap.Application.Ports;

namespace Edumap.Application.UseCases.Instituciones.Queries;

// Query CQRS: Consulta los datos detallados de una institución educativa por su identificador
public record GetInstitucionByIdQuery(int IdInstitucion) : IRequest<InstitucionDetailDto?>;

public record InstitucionDetailDto(
    int IdInstitucion,
    string Nombre,
    string Categoria,
    string TipoGestion,
    string? Descripcion,
    string Direccion,
    string Distrito,
    string Provincia,
    double? Latitud,
    double? Longitud,
    string? Telefono,
    string? Whatsapp,
    string? Email,
    string? SitioWeb,
    string? LogoUrl,
    string Estado,
    decimal? PromedioCalificacion,
    long TotalResenas);

internal sealed class GetInstitucionByIdQueryHandler(IInstitucionQueries queries) : IRequestHandler<GetInstitucionByIdQuery, InstitucionDetailDto?>
{
    public Task<InstitucionDetailDto?> Handle(GetInstitucionByIdQuery request, CancellationToken cancellationToken)
        => queries.GetByIdAsync(request.IdInstitucion, cancellationToken);
}
