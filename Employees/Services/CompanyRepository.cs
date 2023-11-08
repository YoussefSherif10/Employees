using Employees.Data;
using Employees.Interfaces;
using Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext RepositoryContext)
            : base(RepositoryContext) { }

        public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
    }
}
