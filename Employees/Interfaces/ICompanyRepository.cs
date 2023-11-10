using Employees.Models;

namespace Employees.Interfaces
{
    public interface ICompanyRepository
    {
        public IQueryable<Company> GetAllCompanies(bool trackChanges);
        public Task<Company> GetCompanyById(int id, bool track);
        public void CreateCompany(Company company);
    }
}
