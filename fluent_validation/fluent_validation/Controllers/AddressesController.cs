using fluent_validation.Models;
using Microsoft.AspNetCore.Mvc;

namespace fluent_validation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private static readonly List<AddressDto> _addresses = new();

    [HttpGet]
    public IActionResult GetAllAddresses()
    {
        return Ok(_addresses);
    }

    [HttpGet("{id}")]
    public IActionResult GetAddress(int id)
    {
        var address = _addresses.FirstOrDefault(a => a.Id == id);
        return address is not null ? Ok(address) : NotFound(new { Message = "Address not found" });
    }

    [HttpPost]
    public IActionResult CreateAddress([FromBody] AddressDto address)
    {
        address.Id = _addresses.Count + 1; // Simulating Auto-Increment ID
        _addresses.Add(address);
        return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, address);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAddress(int id, [FromBody] AddressDto updatedAddress)
    {
        var address = _addresses.FirstOrDefault(a => a.Id == id);
        if (address is null)
            return NotFound(new { Message = "Address not found" });

        address.Street = updatedAddress.Street;
        address.City = updatedAddress.City;
        address.State = updatedAddress.State;
        address.ZipCode = updatedAddress.ZipCode;

        return Ok(address);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAddress(int id)
    {
        var address = _addresses.FirstOrDefault(a => a.Id == id);
        if (address is null)
            return NotFound(new { Message = "Address not found" });

        _addresses.Remove(address);
        return NoContent();
    }
}
