using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Edumap.Application.Ports;
using Edumap.Domain.Repositories;
using Edumap.Infrastructure.Adapters.Queries;
using Edumap.Infrastructure.Adapters.Repositories;
using Edumap.Infrastructure.Adapters.Security;
using Edumap.Infrastructure.Data;

namespace Edumap.Infrastructure.Configuration;

// Configuración de Infraestructura: inyección de dependencias para DbContext, repositorios y servicios externos
public static class InfrastructureServicesExtensions
{
    public const string ConnectionStringName = "DefaultConnection";

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName);
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                $"Falta la cadena de conexión 'ConnectionStrings:{ConnectionStringName}'. " +
                "Configúrala con 'dotnet user-secrets' o con la variable de entorno ConnectionStrings__DefaultConnection.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        // Persistencia (lado Command)
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Lectura (lado Query)
        services.AddScoped<IInstitucionQueries, InstitucionQueries>();
        services.AddScoped<IResenaQueries, ResenaQueries>();
        services.AddScoped<IUsuarioQueries, UsuarioQueries>();

        // Seguridad
        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .Validate(o => Encoding.UTF8.GetByteCount(o.Key) >= JwtOptions.LongitudMinimaClave,
                $"'{JwtOptions.SectionName}:Key' debe tener al menos {JwtOptions.LongitudMinimaClave} bytes. " +
                "Configúrala con 'dotnet user-secrets' o con la variable de entorno Jwt__Key.")
            .Validate(o => !string.IsNullOrWhiteSpace(o.Issuer) && !string.IsNullOrWhiteSpace(o.Audience),
                $"'{JwtOptions.SectionName}:Issuer' y '{JwtOptions.SectionName}:Audience' son obligatorios.")
            .ValidateOnStart();
        services.AddSingleton<IAuthService, AuthService>();

        return services;
    }
}
