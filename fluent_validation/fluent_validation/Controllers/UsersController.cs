using fluent_validation.Models;
using Microsoft.AspNetCore.Mvc;

namespace fluent_validation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<UserDto> _users = new();

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return user is not null ? Ok(user) : NotFound(new { Message = "User not found" });
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserDto user)
    {
        user.Id = _users.Count + 1; // Simulate auto-increment ID
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserDto updatedUser)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user is null)
            return NotFound(new { Message = "User not found" });

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.Age = updatedUser.Age;

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user is null)
            return NotFound(new { Message = "User not found" });

        _users.Remove(user);
        return NoContent();
    }
}
