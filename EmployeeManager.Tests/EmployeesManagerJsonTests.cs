#region usings

using EmployeeManager.Domain.Models;
using EmployeeManager.Infrastructure.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace EmployeeManager.Tests;

public class EmployeesManagerJsonTests
{
    private readonly EmployeesManagerJson _manager = new(TestFilePath);
    private const string TestFilePath = "testFilePath.json";

    [Fact]
    public async Task FindByIdAsync_ShouldReturnEmployeeJson_WhenEmployeeExists()
    {
        var employee = new Employee { Id = 1, FirstName = "John", LastName = "Doe", SalaryPerHour = 20.0m };
        var employeeJson = JsonConvert.SerializeObject(employee, Formatting.Indented);
        await File.WriteAllTextAsync(TestFilePath, $"[{employeeJson}]");

        var result = await _manager.FindByIdAsync(1);

        Assert.Equal(JObject.Parse(employeeJson).ToString(), result);
    }
}