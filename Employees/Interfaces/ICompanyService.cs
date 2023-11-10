using Employees.Models.DTO;

namespace Employees.Interfaces
{
    public interface ICompanyService
    {
        public Task<IEnumerable<CompanyDto>> GetAllCompanies(bool track);
        public Task<CompanyDto> GetCompanyById(int id, bool track);
        public CompanyDto CreateCompany(CompanyForCreationDto company);
    }
}
