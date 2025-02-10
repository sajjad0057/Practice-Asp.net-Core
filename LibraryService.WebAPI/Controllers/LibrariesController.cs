using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.Services;
using System;

namespace LibraryService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrariesController : ControllerBase
    {
        private readonly ILibrariesService _librariesService;

        public LibrariesController(ILibrariesService librariesService)
        {
            _librariesService = librariesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libraries = await _librariesService.Get(null);
            return Ok(libraries);
        }

        [HttpGet("{libraryId}")]
        public async Task<IActionResult> Get(int libraryId)
        {
            var library = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (library == null)
                return NotFound();
            return Ok(library);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Library l)
        {
            await _librariesService.Add(l);
            return Ok(l);
        }

        [HttpPut("{libraryId}")]
        public async Task<IActionResult> Update(int libraryId, Library library)
        {
            var existingLibrary = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (existingLibrary == null)
                return NotFound();

            await _librariesService.Update(library);
            return NoContent();
        }

        // Implement the DELETE method below
        [HttpDelete("{libraryId}")]
        public async Task<IActionResult> Delete(int libraryId)
        {
            var existingLibrary = (await _librariesService.Get(new[] { libraryId })).FirstOrDefault();
            if (existingLibrary == null)
                return NotFound();

            await _librariesService.Delete(existingLibrary);
            return NoContent();
        }
    }
}
