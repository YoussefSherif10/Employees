using Employees.Models;

namespace Employees.Interfaces
{
    public interface IEmployeeRepository
    {
        public IQueryable<Employee> GetAllEmployees(int companyId, bool trackChanges);
        public Task<Employee> GetEmployeeById(int companyId, int id, bool track);
        public void CreateEmployee(Employee employee);
        public void DeleteEmployee(Employee employee);
        public void UpdateEmployee(Employee employee);
    }
}
