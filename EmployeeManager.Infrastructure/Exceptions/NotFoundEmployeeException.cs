namespace EmployeeManager.Exceptions;

public class NotFoundEmployeeException : Exception
{
    public override string Message => "Сотрудник не был найден.";
}