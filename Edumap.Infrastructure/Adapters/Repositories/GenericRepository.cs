using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Edumap.Domain.Repositories;
using Edumap.Infrastructure.Data;

namespace Edumap.Infrastructure.Adapters.Repositories;

// Adaptador de Persistencia: implementación genérica del repositorio con Entity Framework Core.
// Solo registra cambios en el contexto; la confirmación la hace IUnitOfWork.
internal sealed class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _set = context.Set<T>();

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _set.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _set.ToListAsync(cancellationToken);

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _set.Where(predicate).ToListAsync(cancellationToken);

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => _set.AnyAsync(predicate, cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _set.AddAsync(entity, cancellationToken);

    public void Update(T entity) => _set.Update(entity);

    public void Delete(T entity) => _set.Remove(entity);
}
