using fluent_validation.Models;
using Microsoft.AspNetCore.Mvc;

namespace fluent_validation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser([FromBody] UserDto user)
    {
        return Ok(new { Message = "User created successfully!", User = user });
    }
}
