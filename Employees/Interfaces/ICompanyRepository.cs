using Employees.Models;

namespace Employees.Interfaces
{
    public interface ICompanyRepository
    {
        public IQueryable<Company> GetAllCompanies(bool trackChanges);
    }
}
