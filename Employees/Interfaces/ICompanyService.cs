using Employees.Models.DTO;

namespace Employees.Interfaces
{
    public interface ICompanyService
    {
        public Task<IEnumerable<CompanyDto>> GetAllCompanies(bool track);
        public Task<CompanyDto> GetCompanyById(int id, bool track);
        public Task<CompanyDto> CreateCompany(CompanyForCreationDto company);
        public Task<IEnumerable<CompanyDto>> GetCompaniesByIds(
            IEnumerable<int> ids,
            bool trackChanges
        );
        public Task<(
            IEnumerable<CompanyDto> companyCollection,
            string ids
        )> CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companies);
    }
}
