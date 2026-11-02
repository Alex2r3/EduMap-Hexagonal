using MediatR;
using Edumap.Application.Exceptions;
using Edumap.Domain.Entities;
using Edumap.Domain.Repositories;

namespace Edumap.Application.UseCases.Instituciones.Commands;

// Command CQRS: Recibe datos para registrar una nueva institución educativa y retorna su ID generado
public record CreateInstitucionCommand(
    int IdCategoria,
    int IdDistrito,
    string Nombre,
    string Direccion,
    string TipoGestion = "PRIVADA",
    string? Descripcion = null,
    double? Latitud = null,
    double? Longitud = null,
    string? Telefono = null,
    string? Whatsapp = null,
    string? Email = null,
    string? SitioWeb = null) : IRequest<int>;

internal sealed class CreateInstitucionCommandHandler(
    IGenericRepository<Institucion> instituciones,
    IGenericRepository<Categoria> categorias,
    IGenericRepository<Distrito> distritos,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateInstitucionCommand, int>
{
    public async Task<int> Handle(CreateInstitucionCommand request, CancellationToken cancellationToken)
    {
        var institucion = Institucion.Crear(
            request.IdCategoria, request.IdDistrito, request.Nombre, request.TipoGestion, request.Direccion,
            request.Descripcion, request.Latitud, request.Longitud,
            request.Telefono, request.Whatsapp, request.Email, request.SitioWeb);

        if (!await categorias.ExistsAsync(c => c.IdCategoria == request.IdCategoria, cancellationToken))
            throw new ValidationException($"La categoría {request.IdCategoria} no existe.");

        if (!await distritos.ExistsAsync(d => d.IdDistrito == request.IdDistrito, cancellationToken))
            throw new ValidationException($"El distrito {request.IdDistrito} no existe.");

        await instituciones.AddAsync(institucion, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return institucion.IdInstitucion;
    }
}
