using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Edumap.Infrastructure.Configuration;

// Configuración de Infraestructura: inyección de dependencias para DbContext, repositorios y servicios externos
public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Inyectar DbContext con Npgsql, UnitOfWork, Repositorios y AuthService
        return services;
    }
}
