namespace EmployeeManager.Application.DTOs;

public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal? SalaryPerHour { get; set; }
}