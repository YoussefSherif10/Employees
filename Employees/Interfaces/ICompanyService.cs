using Employees.Models;

namespace Employees.Interfaces
{
    public interface ICompanyService
    {
        public Task<IEnumerable<Company>> GetAllCompanies(bool track);
    }
}
