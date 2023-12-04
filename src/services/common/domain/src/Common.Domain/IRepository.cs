using System.Linq.Expressions;

namespace Common.Domain;

public interface IRepository<TEntity> where TEntity : IEntity
{
    Task<IReadOnlyCollection<TEntity>> GetAllAsync ();
    Task<IReadOnlyCollection<TEntity>> GetAllAsync (Expression<Func<TEntity, bool>> filter);
    Task<TEntity?> GetAsync (Guid id);
    Task<TEntity?> GetAsync (Expression<Func<TEntity, bool>> filter);
    Task CreateAsync (TEntity entity);
    Task UpdateAsync (TEntity entity);
    Task DeleteAsync (Guid id);
}
