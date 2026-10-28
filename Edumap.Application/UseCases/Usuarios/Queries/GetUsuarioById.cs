using MediatR;

namespace Edumap.Application.UseCases.Usuarios.Queries;

// Query CQRS: Obtiene los datos del perfil de un usuario por su ID
public record GetUsuarioByIdQuery(Guid UsuarioId) : IRequest<UsuarioDto?>;

public record UsuarioDto(Guid UsuarioId, string Username, string Email, string Rol);

internal sealed class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioDto?>
{
    public async Task<UsuarioDto?> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return null;
    }
}
