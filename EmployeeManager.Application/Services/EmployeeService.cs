#region usings

using Common;
using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Models;

#endregion

namespace EmployeeManager.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeesFileManger _employeesFileManager;

    public EmployeeService(IEmployeesFileManger employeesFileManager)
    {
        Argument.IsNotNull(employeesFileManager, nameof(employeesFileManager));

        _employeesFileManager = employeesFileManager;
    }

    public async Task<string> FindByIdAsync(int id)
    {
        return await _employeesFileManager.FindByIdAsync(id);
    }

    public async Task<string> GetAllAsync()
    {
        return await _employeesFileManager.GetAllAsync();
    }

    public async Task<string> UpdateEmployeeAsync(UpdateEmployeeDto employeeDto)
    {
        Argument.IsNotNull(employeeDto, nameof(employeeDto));


        return await _employeesFileManager.UpdateAsync(new Employee
        {
            Id = employeeDto.Id,
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            SalaryPerHour = employeeDto.SalaryPerHour
        });
    }

    public async Task<string> AddEmployeeAsync(AddEmployeeDto employeeDto)
    {
        Argument.IsNotNull(employeeDto, nameof(employeeDto));

        return await _employeesFileManager.AddAsync(new Employee
        {
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            SalaryPerHour = employeeDto.SalaryPerHour
        });
    }

    public async Task DeleteByIdAsync(int id)
    {
        await _employeesFileManager.DeleteByIdAsync(id);
    }
}