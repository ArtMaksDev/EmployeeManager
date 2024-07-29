using CommandLine;

namespace ConsoleApp.Options;

[Verb("-delete")]
public class DeleteEmployeeOption
{
    [Option("Id", Required = true)] 
    public int Id { get; set; }

}