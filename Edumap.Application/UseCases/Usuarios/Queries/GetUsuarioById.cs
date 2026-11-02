using MediatR;
using Edumap.Application.Ports;

namespace Edumap.Application.UseCases.Usuarios.Queries;

// Query CQRS: Obtiene los datos del perfil de un usuario por su ID
public record GetUsuarioByIdQuery(int IdUsuario) : IRequest<UsuarioDto?>;

public record UsuarioDto(int IdUsuario, string Nombres, string Apellidos, string Email, string? Telefono, string? FotoUrl, string Rol, string Estado);

internal sealed class GetUsuarioByIdQueryHandler(IUsuarioQueries queries) : IRequestHandler<GetUsuarioByIdQuery, UsuarioDto?>
{
    public Task<UsuarioDto?> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        => queries.GetByIdAsync(request.IdUsuario, cancellationToken);
}
