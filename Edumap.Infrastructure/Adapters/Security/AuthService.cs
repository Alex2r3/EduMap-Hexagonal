using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Edumap.Application.Ports;

namespace Edumap.Infrastructure.Adapters.Security;

// Adaptador de Seguridad: hashing de claves con BCrypt y emisión de tokens JWT firmados con HMAC-SHA256
internal sealed class AuthService(IOptions<JwtOptions> options) : IAuthService
{
    private const int BcryptWorkFactor = 12;

    private readonly JwtOptions _jwt = options.Value;

    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password, BcryptWorkFactor);

    public bool VerifyPassword(string password, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(password, passwordHash);

    public string GenerateJwtToken(int userId, string email, string nombreCompleto, string role)
    {
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwt.Issuer,
            Audience = _jwt.Audience,
            Expires = DateTime.UtcNow.AddMinutes(_jwt.ExpirationMinutes),
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Name, nombreCompleto),
                new Claim("role", role)
            ]),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                SecurityAlgorithms.HmacSha256)
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }
}
