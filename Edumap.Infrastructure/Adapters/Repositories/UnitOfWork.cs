using Microsoft.EntityFrameworkCore;
using Npgsql;
using Edumap.Application.Exceptions;
using Edumap.Domain.Repositories;
using Edumap.Infrastructure.Data;

namespace Edumap.Infrastructure.Adapters.Repositories;

// Adaptador de Persistencia: confirma los cambios y traduce las violaciones de restricciones de PostgreSQL
// a excepciones de aplicación, para que las capas internas no dependan de Npgsql
internal sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && Traducir(pg, ex) is { } traducida)
        {
            throw traducida;
        }
    }

    private static Exception? Traducir(PostgresException pg, DbUpdateException ex) => pg.SqlState switch
    {
        PostgresErrorCodes.UniqueViolation => new ConflictException("Ya existe un registro con esos datos.", ex),
        PostgresErrorCodes.ForeignKeyViolation => new ValidationException("Se hace referencia a un registro que no existe.", ex),
        PostgresErrorCodes.CheckViolation => new ValidationException("Algún valor no cumple las reglas de la base de datos.", ex),
        _ => null
    };
}
