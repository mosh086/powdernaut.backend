namespace Powdernaut.Application.Common.Repository;

public interface IPivotRepository<TEntity> : IUnitOfWork
{
    TEntity Add(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    bool Remove(TEntity entity, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken);

    //IEnumerable<TEntity> Get(CancellationToken cancellationToken);
    //Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken);

    //IQueryable<TEntity> GetAll();
    //IQueryable<TEntity> GetAllAsNoTracking();
    //IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    //Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    //bool Exists(Expression<Func<TEntity, bool>> predicate);
    //Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}
