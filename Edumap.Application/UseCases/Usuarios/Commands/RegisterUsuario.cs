using MediatR;
using Edumap.Application.Exceptions;
using Edumap.Application.Ports;
using Edumap.Domain.Entities;
using Edumap.Domain.Repositories;

namespace Edumap.Application.UseCases.Usuarios.Commands;

// Command CQRS: Registra un nuevo usuario en el sistema y retorna su ID generado
public record RegisterUsuarioCommand(
    string Nombres,
    string Apellidos,
    string Email,
    string Password,
    string? Telefono = null,
    string Rol = Roles.Usuario) : IRequest<int>;

internal sealed class RegisterUsuarioCommandHandler(
    IGenericRepository<Usuario> usuarios,
    IGenericRepository<Rol> roles,
    IUnitOfWork unitOfWork,
    IAuthService authService) : IRequestHandler<RegisterUsuarioCommand, int>
{
    private const int LongitudMinimaPassword = 8;

    // El registro público no permite crear administradores
    private static readonly string[] RolesRegistrables = [Roles.Usuario, Roles.Representante];

    public async Task<int> Handle(RegisterUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < LongitudMinimaPassword)
            throw new ValidationException($"La contraseña debe tener al menos {LongitudMinimaPassword} caracteres.");

        var nombreRol = request.Rol?.Trim().ToUpperInvariant();
        if (nombreRol is null || !RolesRegistrables.Contains(nombreRol))
            throw new ValidationException($"El rol debe ser {string.Join(" o ", RolesRegistrables)}.");

        var email = Usuario.NormalizarEmail(request.Email);
        if (await usuarios.ExistsAsync(u => u.Email.ToLower() == email, cancellationToken))
            throw new ConflictException("Ya existe un usuario registrado con ese email.");

        var rol = (await roles.FindAsync(r => r.Nombre == nombreRol, cancellationToken)).SingleOrDefault()
            ?? throw new InvalidOperationException($"El rol {nombreRol} no está configurado en la base de datos.");

        var usuario = Usuario.Registrar(
            rol.IdRol, request.Nombres, request.Apellidos, email,
            authService.HashPassword(request.Password), request.Telefono);

        await usuarios.AddAsync(usuario, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return usuario.IdUsuario;
    }
}
