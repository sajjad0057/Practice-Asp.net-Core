using CrudwithMongo.Api.Models;
using CrudwithMongo.Api.Repositories;

namespace CrudwithMongo.Api.Services;


public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<Book>> GetAllBooksAsync() => await _bookRepository.GetAllAsync();

    public async Task<Book> GetBookByIdAsync(string id) => await _bookRepository.GetByIdAsync(id);

    public async Task AddBookAsync(Book book) => await _bookRepository.AddAsync(book);

    public async Task UpdateBookAsync(string id, Book book) => await _bookRepository.UpdateAsync(id, book);

    public async Task DeleteBookAsync(string id) => await _bookRepository.DeleteAsync(id);
}
