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

        public void CreateCompany(Company company) => Create(company);

        public IQueryable<Company> GetAllCompanies(bool trackChanges) => FindAll(trackChanges);

        public async Task<Company> GetCompanyById(int id, bool track) =>
            await FindByCondition(c => c.CompanyId.Equals(id), track).SingleAsync();
    }
}
