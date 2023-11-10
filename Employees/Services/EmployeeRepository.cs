using Employees.Data;
using Employees.Interfaces;
using Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext RepositoryContext)
            : base(RepositoryContext) { }

        public void CreateEmployee(Employee employee) => Create(employee);

        public void DeleteEmployee(Employee employee) => Delete(employee);

        public IQueryable<Employee> GetAllEmployees(int companyId, bool trackChanges) =>
            FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges);

        public async Task<Employee> GetEmployeeById(int companyId, int id, bool track)
        {
            var employees = FindByCondition(e => e.CompanyId.Equals(companyId), track);
            return await employees.SingleAsync(e => e.EmployeeId == id);
        }

        public void UpdateEmployee(Employee employee) => Update(employee);
    }
}
