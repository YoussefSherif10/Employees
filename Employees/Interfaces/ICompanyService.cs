using Employees.Models.DTO;
using Employees.Models.Params;

namespace Employees.Interfaces
{
    public interface ICompanyService
    {
        public Task<(
            IEnumerable<CompanyDto> companyDtos,
            PagingInfoDto pagingInfoDto
        )> GetAllCompanies(CompanyParams companyParams, bool track);
        public Task<CompanyDto> GetCompanyById(int id, CompanyParams companyParams, bool track);
        public Task<CompanyDto> CreateCompany(CompanyForCreationDto company);
        public Task<IEnumerable<CompanyDto>> GetCompaniesByIds(
            IEnumerable<int> ids,
            CompanyParams companyParams,
            bool trackChanges
        );
        public Task<(
            IEnumerable<CompanyDto> companyCollection,
            string ids
        )> CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companies);

        public Task DeleteCompany(int id);
        public Task UpdateCompany(int companyId, CompanyForUpdateDto companyForUpdate);
    }
}
