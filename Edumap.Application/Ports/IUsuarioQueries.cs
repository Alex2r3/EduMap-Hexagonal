using Edumap.Application.UseCases.Usuarios.Queries;

namespace Edumap.Application.Ports;

// Puerto de Lectura (CQRS): consultas de usuarios proyectadas directamente a DTOs (nunca expone el hash de la clave)
public interface IUsuarioQueries
{
    Task<UsuarioDto?> GetByIdAsync(int idUsuario, CancellationToken cancellationToken);
}
