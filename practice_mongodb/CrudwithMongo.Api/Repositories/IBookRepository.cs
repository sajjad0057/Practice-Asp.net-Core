using CrudwithMongo.Api.Models;

namespace CrudwithMongo.Api.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(string id);
    Task AddAsync(Book book);
    Task UpdateAsync(string id, Book book);
    Task DeleteAsync(string id);
}
