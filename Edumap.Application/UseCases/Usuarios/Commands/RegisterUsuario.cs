using MediatR;
using Edumap.Application.Ports;
using Edumap.Domain.Repositories;

namespace Edumap.Application.UseCases.Usuarios.Commands;

// Command CQRS: Registra un nuevo usuario en el sistema sin devolver datos (solo confirmación)
public record RegisterUsuarioCommand(string Username, string Email, string Password, string Rol) : IRequest;

internal sealed class RegisterUsuarioCommandHandler(IUnitOfWork unitOfWork, IAuthService authService) : IRequestHandler<RegisterUsuarioCommand>
{
    public async Task Handle(RegisterUsuarioCommand request, CancellationToken cancellationToken)
    {
        // TODO: Hashear clave con authService, crear usuario y guardar con unitOfWork
        var hash = authService.HashPassword(request.Password);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
