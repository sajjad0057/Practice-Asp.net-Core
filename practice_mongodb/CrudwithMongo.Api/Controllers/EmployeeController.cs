using CrudwithMongo.Api.Models;
using CrudwithMongo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudwithMongo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAllEmployees() => await _employeeService.GetAllEmployeesAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(string id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null) return NotFound();
        return employee;
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee(Employee employee)
    {
        await _employeeService.AddEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateEmployee(string id, Employee employee)
    {
        await _employeeService.UpdateEmployeeAsync(id, employee);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        await _employeeService.DeleteEmployeeAsync(id);
        return NoContent();
    }
}