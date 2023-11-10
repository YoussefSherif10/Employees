namespace Employees.Models.DTO
{
    [Serializable]
    public record EmployeeDto(int EmployeeId, string Name, string Position, int Age);
}
