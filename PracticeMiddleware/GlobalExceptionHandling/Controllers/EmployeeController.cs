using GlobalExceptionHandling.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            var testCookie = Request.Cookies["test_key"];
            Console.WriteLine($"All Cookies : {JsonSerializer.Serialize(Request.Cookies)}");
            Console.WriteLine($"testCookie : {testCookie}");

            throw new Exception("Intentional Exception occurred!");
            return Ok(new { Message = "Employee created successfully", Employee = employee});
        }
    }
}
