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

        public void DeleteCompany(Company company) => Delete(company);

        public IQueryable<Company> GetAllCompanies(in bool trackChanges) => FindAll(trackChanges);

        public IQueryable<Company> GetCompaniesByIds(IEnumerable<int> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.CompanyId), trackChanges);

        public async Task<Company> GetCompanyById(int id, bool track) =>
            await FindByCondition(c => c.CompanyId.Equals(id), track).SingleAsync();

        public void UpdateCompany(Company company) => Update(company);
    }
}
