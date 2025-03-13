namespace CrudwithMongo.Api.Services;

using CrudwithMongo.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class BookService
{
    private readonly IMongoCollection<Book> _booksCollection;

    public BookService(IOptions<BookStoreDatabaseSettings> settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _booksCollection = database.GetCollection<Book>(settings.Value.CollectionName);
    }

    public async Task<List<Book>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Book> GetAsync(string id) =>
        await _booksCollection.Find(book => book.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book book) =>
        await _booksCollection.InsertOneAsync(book);

    public async Task UpdateAsync(string id, Book book) =>
        await _booksCollection.ReplaceOneAsync(b => b.Id == id, book);

    public async Task DeleteAsync(string id) =>
        await _booksCollection.DeleteOneAsync(book => book.Id == id);
}
