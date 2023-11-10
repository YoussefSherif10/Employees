using Employees.Models;

namespace Employees.Interfaces
{
    public interface ICompanyRepository
    {
        public IQueryable<Company> GetAllCompanies(in bool trackChanges);
        public Task<Company> GetCompanyById(int id, bool track);
        public void CreateCompany(Company company);
        public IQueryable<Company> GetCompaniesByIds(IEnumerable<int> ids, bool trackChanges);
        public void DeleteCompany(Company company);
        public void UpdateCompany(Company company);
    }
}
