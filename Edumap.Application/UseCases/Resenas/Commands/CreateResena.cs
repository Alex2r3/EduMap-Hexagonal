using MediatR;
using Edumap.Domain.Repositories;

namespace Edumap.Application.UseCases.Resenas.Commands;

// Command CQRS: Registra una nueva reseña o calificación para una institución educativa
public record CreateResenaCommand(Guid InstitucionId, Guid UsuarioId, int Calificacion, string Comentario) : IRequest<Guid>;

internal sealed class CreateResenaCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateResenaCommand, Guid>
{
    public async Task<Guid> Handle(CreateResenaCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Guid.NewGuid();
    }
}
