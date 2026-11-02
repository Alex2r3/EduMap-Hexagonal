using MediatR;
using Edumap.Application.Exceptions;
using Edumap.Domain.Entities;
using Edumap.Domain.Repositories;

namespace Edumap.Application.UseCases.Resenas.Commands;

// Command CQRS: Registra una nueva reseña o calificación para una institución educativa
public record CreateResenaCommand(int IdInstitucion, int IdUsuario, short Calificacion, string? Comentario) : IRequest<int>;

internal sealed class CreateResenaCommandHandler(
    IGenericRepository<Resena> resenas,
    IGenericRepository<Institucion> instituciones,
    IGenericRepository<Usuario> usuarios,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateResenaCommand, int>
{
    public async Task<int> Handle(CreateResenaCommand request, CancellationToken cancellationToken)
    {
        var resena = Resena.Crear(request.IdInstitucion, request.IdUsuario, request.Calificacion, request.Comentario);

        if (!await instituciones.ExistsAsync(i => i.IdInstitucion == request.IdInstitucion, cancellationToken))
            throw new ValidationException($"La institución {request.IdInstitucion} no existe.");

        if (!await usuarios.ExistsAsync(u => u.IdUsuario == request.IdUsuario, cancellationToken))
            throw new ValidationException($"El usuario {request.IdUsuario} no existe.");

        if (await resenas.ExistsAsync(r => r.IdUsuario == request.IdUsuario && r.IdInstitucion == request.IdInstitucion, cancellationToken))
            throw new ConflictException("El usuario ya registró una reseña para esta institución.");

        await resenas.AddAsync(resena, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return resena.IdResena;
    }
}
