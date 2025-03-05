using GlobalExceptionHandling.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlobalExceptionHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        /// <summary>
        /// Create a new employee test purpose
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>result of the operations</returns>
        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            return Ok(new { Message = "Employee created successfully", Employee = employee});
        }
    }
}
