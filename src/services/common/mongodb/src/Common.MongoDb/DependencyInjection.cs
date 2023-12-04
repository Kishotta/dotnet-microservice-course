using Common.Domain;
using Common.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Common.MongoDb;

public static class DependencyInjection
{
    public static IServiceCollection AddMongoDb (this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureMicroservice (configuration);

        services.AddOptions<Options.MongoDb> ()
                .Bind (configuration.GetSection (nameof(Options.MongoDb)))
                .ValidateDataAnnotations ();

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        services.AddSingleton<IMongoDatabase> (serviceProvider =>
        {
            var serviceOptions = serviceProvider.GetRequiredService<IOptions<Service.Options.Service>> ().Value;
            var mongoDbOptions = serviceProvider.GetRequiredService<IOptions<Options.MongoDb>> ().Value;

            var client = new MongoClient (mongoDbOptions.ConnectionString);
            return client.GetDatabase (serviceOptions.Name);
        });

        return services;
    }

    public static IServiceCollection AddMongoDbRepository<TEntity> (this IServiceCollection services, string collectionName)
        where TEntity : IEntity
    {
        services.AddSingleton<IRepository<TEntity>> (serviceProvider =>
        {
            var mongoDatabase = serviceProvider.GetRequiredService<IMongoDatabase> ();
            return new MongoRepository<TEntity> (mongoDatabase, collectionName);
        });

        return services;
    }
}
