using CrudwithMongo.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrudwithMongo.Api.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _booksCollection;

    public BookRepository(IOptions<BookStoreDatabaseSettings> settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _booksCollection = database.GetCollection<Book>(settings.Value.CollectionName);
    }

    public async Task<List<Book>> GetAllAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Book> GetByIdAsync(string id) =>
        await _booksCollection.Find(book => book.Id == id).FirstOrDefaultAsync();

    public async Task AddAsync(Book book) =>
        await _booksCollection.InsertOneAsync(book);

    public async Task UpdateAsync(string id, Book book) =>
        await _booksCollection.ReplaceOneAsync(b => b.Id == id, book);

    public async Task DeleteAsync(string id) =>
        await _booksCollection.DeleteOneAsync(book => book.Id == id);
}
