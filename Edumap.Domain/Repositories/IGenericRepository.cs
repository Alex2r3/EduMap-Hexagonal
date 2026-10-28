using System.Linq.Expressions;

namespace Edumap.Domain.Repositories;

// Puerto de Persistencia: operaciones genéricas de acceso a datos que la Infraestructura implementa
public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
