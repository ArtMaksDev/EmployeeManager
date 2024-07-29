using CommandLine;

namespace ConsoleApp.Options;

[Verb("-update")]
public class UpdateEmployeeOption
{
    [Option("Id", Required = true)]
    public int Id { get; set; }

    [Option("FirstName")]
    public string? FirstName { get; set; }

    [Option("LastName")]
    public string? LastName { get; set; }

    [Option("Salary")]
    public decimal SalaryPerHour { get; set; }
}