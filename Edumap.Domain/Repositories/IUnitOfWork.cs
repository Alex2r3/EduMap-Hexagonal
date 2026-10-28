namespace Edumap.Domain.Repositories;

// Puerto de Persistencia: abstracción de la unidad de trabajo para confirmar transacciones
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
