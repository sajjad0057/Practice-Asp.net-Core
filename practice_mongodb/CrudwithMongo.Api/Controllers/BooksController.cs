using CrudwithMongo.Api.Models;
using CrudwithMongo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudwithMongo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<List<Book>> Get() => await _bookService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _bookService.GetAsync(id);
        if (book == null) return NotFound();
        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        await _bookService.CreateAsync(book);
        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Book book)
    {
        var existingBook = await _bookService.GetAsync(id);
        if (existingBook == null) return NotFound();

        book.Id = existingBook.Id;
        await _bookService.UpdateAsync(id, book);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _bookService.GetAsync(id);
        if (book == null) return NotFound();

        await _bookService.DeleteAsync(id);
        return NoContent();
    }
}