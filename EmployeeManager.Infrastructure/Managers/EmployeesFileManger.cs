#region usings

using Common;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Models;

#endregion

namespace EmployeeManager.Infrastructure.Managers;

public abstract class EmployeesFileManger : IEmployeesFileManger
{
    protected readonly string Path;

    protected EmployeesFileManger(string path)
    {
        Argument.IsNotNullOrEmpty(path, nameof(path));

        Path = path;
    }

    public abstract Task<string> FindByIdAsync(int id);
    public abstract Task<string> GetAllAsync();

    public abstract Task<string> UpdateAsync(Employee employee);
    public abstract Task<string> AddAsync(Employee employee);

    public abstract Task DeleteByIdAsync(int id);
}