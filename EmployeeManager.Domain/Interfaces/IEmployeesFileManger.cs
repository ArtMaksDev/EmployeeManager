#region usings

using EmployeeManager.Domain.Models;

#endregion

namespace EmployeeManager.Domain.Interfaces;

public interface IEmployeesFileManger
{
    public Task<string> FindByIdAsync(int id);
    public Task<string> GetAllAsync();

    public Task<string> UpdateAsync(Employee employee);
    public Task<string> AddAsync(Employee employee);

    public Task DeleteByIdAsync(int id);
}