using FilterWithExHndlrMiddleware.Filters;
using FilterWithExHndlrMiddleware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilterWithExHndlrMiddleware.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateBook([FromBody] BookDto book)
    {
        ////throw new Exception("Test Exception");
        Console.WriteLine($"[BookController.CreateBook] -> Executed");
        return Ok(book);
    }
}
