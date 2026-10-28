using MediatR;
using Edumap.Domain.Repositories;

namespace Edumap.Application.UseCases.Instituciones.Commands;

// Command CQRS: Recibe datos para registrar una nueva institución educativa y retorna su ID generado
public record CreateInstitucionCommand(string Nombre, string Direccion, string Telefono, Guid DistritoId) : IRequest<Guid>;

internal sealed class CreateInstitucionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateInstitucionCommand, Guid>
{
    public async Task<Guid> Handle(CreateInstitucionCommand request, CancellationToken cancellationToken)
    {
        // TODO: Mapear a entidad de dominio, agregar al repositorio y persistir con unitOfWork
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Guid.NewGuid();
    }
}
