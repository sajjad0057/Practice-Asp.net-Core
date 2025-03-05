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
    [ValidateModelState]
    public IActionResult CreateBook([FromBody] BookDto book)
    {
        // If ModelState is invalid, the ValidateModelStateFilter will handle the response
        return Ok(book);
    }
}
