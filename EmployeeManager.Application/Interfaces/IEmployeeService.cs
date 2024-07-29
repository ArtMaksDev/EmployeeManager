using EmployeeManager.Application.DTOs;

namespace EmployeeManager.Application.Interfaces;

public interface IEmployeeService
{
    public Task<string> FindByIdAsync(int id);
    public Task<string> GetAllAsync();
    public Task<string> UpdateEmployeeAsync(UpdateEmployeeDto employeeDto);

    public Task<string> AddEmployeeAsync(AddEmployeeDto employeeDto);

    public Task DeleteByIdAsync(int id);


}