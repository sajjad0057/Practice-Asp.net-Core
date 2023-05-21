using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestEnumValidation.Models;

namespace TestEnumValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        public PersonsController() { }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] Person person)
        {
            //throw new Exception("Throw a intentional exception..");
            await Task.CompletedTask;
            return Ok(person);
        }
    }
}
