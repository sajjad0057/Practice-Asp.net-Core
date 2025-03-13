using CrudwithMongo.Api.Models;

namespace CrudwithMongo.Api.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(string id);
    Task AddEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(string id, Employee employee);
    Task DeleteEmployeeAsync(string id);
}