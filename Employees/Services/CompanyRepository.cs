using Employees.Data;
using Employees.Interfaces;
using Employees.Models;

namespace Employees.Services
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext RepositoryContext)
            : base(RepositoryContext) { }
    }
}
