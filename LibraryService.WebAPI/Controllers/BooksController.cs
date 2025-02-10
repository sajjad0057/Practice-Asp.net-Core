using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService, ILibrariesService librariesService)
        {
            _librariesService = librariesService;
            _booksService = booksService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(string libraryId)
        {
            var lId = int.Parse(libraryId);
            var books = await _booksService.Get(lId, null);
            return Ok(books);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Book b)
        {
            await _booksService.Add(b);
            return Ok(b);
        }
    }
}