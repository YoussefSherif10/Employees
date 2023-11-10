using Employees.Models.DTO;

namespace Employees.Interfaces
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeDto>> GetAllEmployees(int companyId, bool trackChanges);
        public Task<EmployeeDto> GetEmployeeById(int companyId, int id, bool track);
        public EmployeeDto CreateEmployee(int companyId, EmployeeForCreationDto employee);
    }
}
