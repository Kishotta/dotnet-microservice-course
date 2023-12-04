using System.Linq.Expressions;
using Common.Domain;
using MongoDB.Driver;

namespace Common.MongoDb;

public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
{
    private readonly IMongoCollection<TEntity>        _collection;
    private readonly FilterDefinitionBuilder<TEntity> _filterBuilder = Builders<TEntity>.Filter;

    public MongoRepository (IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TEntity> (collectionName);
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync ()
    {
        return await _collection.Find (_filterBuilder.Empty).ToListAsync ();
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync (Expression<Func<TEntity, bool>> filter)
    {
        return await _collection.Find (filter).ToListAsync ();
    }

    public async Task<TEntity?> GetAsync (Guid id)
    {
        var filter = _filterBuilder.Eq (entity => entity.Id, id);
        return await _collection.Find (filter).SingleOrDefaultAsync ();
    }

    public async Task<TEntity?> GetAsync (Expression<Func<TEntity, bool>> filter)
    {
        return await _collection.Find (filter).SingleOrDefaultAsync ();
    }

    public async Task CreateAsync (TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException (nameof (entity));

        await _collection.InsertOneAsync (entity);
    }

    public async Task UpdateAsync (TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException (nameof (entity));

        var filter = _filterBuilder.Eq (existingEntity => existingEntity.Id, entity.Id);
        await _collection.ReplaceOneAsync (filter, entity);
    }

    public async Task DeleteAsync (Guid id)
    {
        var filter = _filterBuilder.Eq (entity => entity.Id, id);
        await _collection.DeleteOneAsync (filter);
    }
}
