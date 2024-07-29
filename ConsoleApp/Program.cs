#region usings

using System.Collections.Specialized;
using System.Configuration;
using CommandLine;
using ConsoleApp.Options;
using EmployeeManager.Application.DTOs;
using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Services;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Exceptions;
using EmployeeManager.Infrastructure.Managers;

#endregion

namespace ConsoleApp;

internal class Program
{
    public static NameValueCollection AppSettings = ConfigurationManager.AppSettings;

    public static IEmployeesFileManger EmployeesFileManger { get; }
        = new EmployeesManagerJson(AppSettings["employeesListPath"]!);

    public static IEmployeeService EmployeeService { get; }
        = new EmployeeService(EmployeesFileManger);

    private static void Main(string[] args)
    {
        try
        {
            Parser.Default
                .ParseArguments<AddEmployeeOption, UpdateEmployeeOption, GetEmployeeOption, GetAllEmployeeOption,
                    DeleteEmployeeOption>(
                    ReplaceColonWithDoubleDash(args))
                .MapResult(
                    (AddEmployeeOption options) => AddEmployee(options).GetAwaiter().GetResult(),
                    (GetEmployeeOption options) => GetEmployee(options).GetAwaiter().GetResult(),
                    (GetAllEmployeeOption options) => GetAllEmployee(options).GetAwaiter().GetResult(),
                    (UpdateEmployeeOption options) => UpdateEmployee(options).GetAwaiter().GetResult(),
                    (DeleteEmployeeOption options) => DeleteEmployee(options).GetAwaiter().GetResult(),
                    _ => 1
                );
        }
        catch (NotFoundEmployeeException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static string[] ReplaceColonWithDoubleDash(string[] args)
    {
        return args.Select(arg =>
        {
            if (!arg.Contains(':'))
            {
                return arg;
            }

            var parts = arg.Split(':');

            return $"--{parts[0]}={parts[1]}";
        }).ToArray();
    }

    private static async Task<int> GetAllEmployee(GetAllEmployeeOption options)
    {
        await Console.Out.WriteAsync(
            await EmployeeService.GetAllAsync()
        );

        return 0;
    }

    private static async Task<int> GetEmployee(GetEmployeeOption options)
    {
        await Console.Out.WriteAsync(
            await EmployeeService.FindByIdAsync(options.Id)
        );

        return 0;
    }

    private static async Task<int> DeleteEmployee(DeleteEmployeeOption options)
    {
        await EmployeeService.DeleteByIdAsync(options.Id);

        return 0;
    }

    private static async Task<int> AddEmployee(AddEmployeeOption options)
    {
        await Console.Out.WriteLineAsync(
            await EmployeeService.AddEmployeeAsync(new AddEmployeeDto
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                SalaryPerHour = options.SalaryPerHour
            }));

        return 0;
    }

    private static async Task<int> UpdateEmployee(UpdateEmployeeOption options)
    {
        Console.WriteLine(
            await EmployeeService.UpdateEmployeeAsync(new UpdateEmployeeDto
            {
                Id = options.Id,
                FirstName = options.FirstName,
                LastName = options.LastName,
                SalaryPerHour = options.SalaryPerHour
            })
        );

        return 0;
    }
}