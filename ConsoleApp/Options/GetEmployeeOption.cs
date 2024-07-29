#region usings

using CommandLine;

#endregion

namespace ConsoleApp.Options;

[Verb("-get")]
public class GetEmployeeOption
{
    [Option("Id", Required = true)] 
    public int Id { get; set; }
}