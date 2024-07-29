#region usings

using Common;
using EmployeeManager.Domain.Models;
using EmployeeManager.Exceptions;
using EmployeeManager.Infrastructure.Helpers.Updaters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace EmployeeManager.Infrastructure.Managers;

public class EmployeesManagerJson : EmployeesFileManger
{
    public readonly string TempPath;

    public EmployeesManagerJson(string path) : base(path)
    {
        Argument.IsNotNullOrEmpty(path, nameof(path));

        TempPath = Path + ".tmp";
    }

    private async Task<JArray> LoadEmployeesAsync()
    {
        using var reader = new StreamReader(Path);
        await using var jsonReader = new JsonTextReader(reader);

        return await JArray.LoadAsync(jsonReader);
    }

    private async Task SaveTempEmployeesAsync(JToken employees)
    {
        Argument.IsNotNull(employees, nameof(employees));

        await using var writer = new StreamWriter(TempPath);
        await using var jsonWriter = new JsonTextWriter(writer);
        jsonWriter.Formatting = Formatting.Indented;

        await employees.WriteToAsync(jsonWriter);
        await writer.FlushAsync();
    }

    private void ReplaceFile()
    {
        File.Delete(Path);
        File.Move(TempPath, Path);
    }

    public override async Task<string> FindByIdAsync(int id)
    {
        await using var fileStream = File.OpenRead(Path);
        using var reader = new StreamReader(fileStream);
        await using var jsonReader = new JsonTextReader(reader);

        while (await jsonReader.ReadAsync())
        {
            if (jsonReader.TokenType != JsonToken.StartObject)
            {
                continue;
            }

            var currentEmployee = await JObject.LoadAsync(jsonReader);

            if (currentEmployee[nameof(Employee.Id)]?.Value<int>() == id)
            {
                return currentEmployee.ToString();
            }
        }

        throw new NotFoundEmployeeException();
    }

    public override async Task<string> GetAllAsync()
    {
        return await File.ReadAllTextAsync(Path);
    }

    public override async Task<string> UpdateAsync(Employee employee)
    {
        Argument.IsNotNull(employee, nameof(employee));

        var employees = await LoadEmployeesAsync();
        var currentStateEmployee = employees
                .FirstOrDefault(e => e[nameof(employee.Id)]?.Value<int>() == employee.Id)
            ?? throw new NotFoundEmployeeException();

        new EmployeeUpdater().Update(currentStateEmployee, employee);

        await SaveTempEmployeesAsync(employees);
        ReplaceFile();

        return currentStateEmployee.ToString();
    }

    public override async Task<string> AddAsync(Employee employee)
    {
        Argument.IsNotNull(employee, nameof(employee));

        var newEmployeeJson = JsonConvert.SerializeObject(employee, Formatting.Indented);

        await using var fileStream = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

        fileStream.Seek(-1, SeekOrigin.End);

        await using var writer = new StreamWriter(fileStream);

        if (fileStream.Position > 1)
        {
            await writer.WriteAsync(",");
        }

        await writer.WriteAsync(newEmployeeJson);
        await writer.WriteAsync("]");

        return newEmployeeJson;
    }

    public override async Task DeleteByIdAsync(int id)
    {
        var employees = await LoadEmployeesAsync();
        var deleteEmployee = employees
                .FirstOrDefault(e => e[nameof(Employee.Id)]?.Value<int>() == id)
            ?? throw new NotFoundEmployeeException();

        employees.Remove(deleteEmployee);

        await SaveTempEmployeesAsync(employees);
        ReplaceFile();
    }
}
