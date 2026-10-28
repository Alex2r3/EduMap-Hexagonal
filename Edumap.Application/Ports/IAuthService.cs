namespace Edumap.Application.Ports;

// Puertos de Aplicación: contratos para servicios externos (autenticación, correos, notificaciones)
public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
    string GenerateJwtToken(Guid userId, string username, string email, string role);
}
