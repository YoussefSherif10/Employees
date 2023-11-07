using Employees.Data;
using Employees.Interfaces;
using Employees.Models;

namespace Employees.Services
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext RepositoryContext)
            : base(RepositoryContext) { }
    }
}
