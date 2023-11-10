namespace Employees.Models.DTO
{
    public record CompanyForUpdateDto(
        string Name,
        string Address,
        string Country,
        IEnumerable<EmployeeForCreationDto> Employees
    );
}
