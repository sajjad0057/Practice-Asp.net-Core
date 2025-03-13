using CrudwithMongo.Api.DBSettings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CrudwithMongo.Api.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public Repository(IOptions<MongoDBSettings> settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);

        // Dynamically resolve the collection name from the class name
        string collectionName = typeof(T).Name + "s"; // Example: Book -> Books

        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

    public async Task<T> GetByIdAsync(string id) =>
        await _collection.Find(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id))).FirstOrDefaultAsync();

    public async Task AddAsync(T entity) => await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(string id, T entity) =>
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)), entity);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)));
}