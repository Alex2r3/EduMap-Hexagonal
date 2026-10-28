using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Edumap.Api.Configuration;

// Configuración de la API: registro de Swagger, autenticación JWT y registro de MediatR con sus handlers
public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Registro de MediatR para escanear los handlers de la capa Application
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Edumap.Application.DTOs.ApiResponseDto).Assembly));
        return services;
    }
}
