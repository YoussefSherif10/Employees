using Employees.Models.DTO;

namespace Employees.Interfaces
{
    public interface ICompanyService
    {
        public Task<IEnumerable<CompanyDto>> GetAllCompanies(bool track);
    }
}
