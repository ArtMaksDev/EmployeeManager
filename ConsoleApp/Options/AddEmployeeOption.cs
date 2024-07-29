using CommandLine;

namespace ConsoleApp.Options;

[Verb("-add")]
public class AddEmployeeOption
{
    [Option("FirstName", Required = true)]
    public string? FirstName { get; set; }

    [Option("LastName", Required = true)]
    public string? LastName { get; set; }

    [Option("Salary", Required = true)]
    public decimal SalaryPerHour { get; set; }
}