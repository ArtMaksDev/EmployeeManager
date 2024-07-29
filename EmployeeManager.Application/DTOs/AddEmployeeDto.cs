namespace EmployeeManager.Application.DTOs;

public class AddEmployeeDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal? SalaryPerHour { get; set; }
}