using Employees.Models.DTO;
using Employees.Models.Params;

namespace Employees.Interfaces
{
    public interface IEmployeeService
    {
        public Task<(
            IEnumerable<EmployeeDto> employeeDtos,
            PagingInfoDto pagingInfoDto
        )> GetAllEmployees(int companyId, EmployeeParams employeeParams, bool trackChanges);
        public Task<EmployeeDto> GetEmployeeById(int companyId, int id, bool track);
        public EmployeeDto CreateEmployee(int companyId, EmployeeForCreationDto employee);
        public Task DeleteEmployee(int companyId, int id);
        public Task UpdateEmployee(int companyId, int id, EmployeeForUpdateDto employee);
    }
}
