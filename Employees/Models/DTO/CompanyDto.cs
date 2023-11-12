namespace Employees.Models.DTO
{
    [Serializable]
    public record CompanyDto(
        int CompanyId,
        string Name,
        string FullAddress,
        IEnumerable<EmployeeDto>? Employees = null
    );
}
