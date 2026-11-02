using Microsoft.EntityFrameworkCore;
using Edumap.Application.Ports;
using Edumap.Application.UseCases.Usuarios.Queries;
using Edumap.Infrastructure.Data;

namespace Edumap.Infrastructure.Adapters.Queries;

// Adaptador de Lectura (CQRS): proyecta el perfil del usuario a DTO, sin incluir el hash de la clave
internal sealed class UsuarioQueries(ApplicationDbContext context) : IUsuarioQueries
{
    public Task<UsuarioDto?> GetByIdAsync(int idUsuario, CancellationToken cancellationToken)
        => context.Usuarios
            .AsNoTracking()
            .Where(u => u.IdUsuario == idUsuario)
            .Select(u => new UsuarioDto(
                u.IdUsuario,
                u.Nombres,
                u.Apellidos,
                u.Email,
                u.Telefono,
                u.FotoUrl,
                u.Rol.Nombre,
                u.Estado))
            .FirstOrDefaultAsync(cancellationToken);
}
