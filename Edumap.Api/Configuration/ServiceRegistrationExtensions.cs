using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Edumap.Infrastructure.Adapters.Security;

namespace Edumap.Api.Configuration;

// Configuración de la API: registro de Swagger, autenticación JWT y registro de MediatR con sus handlers
public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Registro de MediatR para escanear los handlers de la capa Application
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Edumap.Application.DTOs.ApiResponseDto).Assembly));

        // Autenticación JWT: valida los tokens con la misma configuración (ya validada) que usa AuthService para emitirlos
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<JwtOptions>>((bearer, jwtOptions) =>
            {
                var jwt = jwtOptions.Value;
                bearer.MapInboundClaims = false;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    ValidateLifetime = true,
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            });
        services.AddAuthorization();

        return services;
    }
}
