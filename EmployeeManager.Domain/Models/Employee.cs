namespace EmployeeManager.Domain.Models;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal? SalaryPerHour { get; set; }

    public override string ToString()
    {
        return
            $@"
            {nameof(FirstName)} = {FirstName}, 
            {nameof(LastName)} = {LastName}, 
            {nameof(SalaryPerHour)} = {SalaryPerHour}";
    }
}