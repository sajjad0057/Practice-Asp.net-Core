using CrudwithMongo.Api.Models;

namespace CrudwithMongo.Api.Services;

public interface IBookService
{
    Task<List<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(string id);
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(string id, Book book);
    Task DeleteBookAsync(string id);
}