using Employees.Models;

namespace Employees.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges);
    }
}
