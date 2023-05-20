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
        public IActionResult CreatePerson([FromBody] Person person)
        {
            return Ok(person);
        }
    }
}
