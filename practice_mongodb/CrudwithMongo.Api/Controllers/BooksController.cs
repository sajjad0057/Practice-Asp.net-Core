using CrudwithMongo.Api.Models;
using CrudwithMongo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudwithMongo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAllBooks() => await _bookService.GetAllBooksAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Book>> GetBookById(string id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null) return NotFound();
        return book;
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(Book book)
    {
        await _bookService.AddBookAsync(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateBook(string id, Book book)
    {
        await _bookService.UpdateBookAsync(id, book);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteBook(string id)
    {
        await _bookService.DeleteBookAsync(id);
        return NoContent();
    }
}