using CrudwithMongo.Api.Models;
using CrudwithMongo.Api.Repositories;

namespace CrudwithMongo.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _employeeRepository;

    public EmployeeService(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync() => await _employeeRepository.GetAllAsync();

    public async Task<Employee> GetEmployeeByIdAsync(string id) => await _employeeRepository.GetByIdAsync(id);

    public async Task AddEmployeeAsync(Employee employee) => await _employeeRepository.AddAsync(employee);

    public async Task UpdateEmployeeAsync(string id, Employee employee) => await _employeeRepository.UpdateAsync(id, employee);

    public async Task DeleteEmployeeAsync(string id) => await _employeeRepository.DeleteAsync(id);
}
