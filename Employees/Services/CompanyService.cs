using Employees.Extensions;
using Employees.Interfaces;
using Employees.Models;
using Employees.Models.DTO;
using Employees.Models.Params;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;

        public CompanyService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<CompanyDto> CreateCompany(CompanyForCreationDto company)
        {
            var entity = new Company
            {
                Name = company.Name,
                Address = company.Address,
                Country = company.Country,
                Employees = company
                    .Employees
                    .Select(
                        e =>
                            new Employee
                            {
                                Name = e.Name,
                                Position = e.Position,
                                Age = e.Age
                            }
                    )
                    .ToList()
            };

            _repository.Company.CreateCompany(entity);
            await _repository.Save();

            return new CompanyDto(
                entity.CompanyId,
                entity.Name,
                string.Join(", ", entity.Address, entity.Country)
            );
        }

        public async Task<(
            IEnumerable<CompanyDto> companyCollection,
            string ids
        )> CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companies)
        {
            var entities = companies.Select(
                c =>
                    new Company
                    {
                        Name = c.Name,
                        Address = c.Address,
                        Country = c.Country,
                        Employees = c.Employees
                            .Select(
                                e =>
                                    new Employee
                                    {
                                        Name = e.Name,
                                        Age = e.Age,
                                        Position = e.Position
                                    }
                            )
                            .ToList()
                    }
            );

            foreach (var entity in entities)
                _repository.Company.CreateCompany(entity);

            await _repository.Save();

            return (
                companyCollection: entities.Select(
                    e =>
                        new CompanyDto(e.CompanyId, e.Name, string.Join(", ", e.Address, e.Country))
                ),
                ids: string.Join(",", entities.Select(e => e.CompanyId))
            );
        }

        public async Task DeleteCompany(int id)
        {
            var company = new Company { CompanyId = id };
            _repository.Company.DeleteCompany(company);
            await _repository.Save();
        }

        public async Task<(
            IEnumerable<CompanyDto> companyDtos,
            PagingInfoDto pagingInfoDto
        )> GetAllCompanies(CompanyParams companyParams, bool track)
        {
            var companies = await _repository
                .Company
                .GetAllCompanies(track)
                .SortCompanies(companyParams.SortBy)
                .Pagination(companyParams.PageNumber, companyParams.PageSize)
                .FilterCompanies(
                    companyParams.FilterBy,
                    companyParams.FilterValue,
                    companyParams.MinEmployees
                )
                .SearchCompanies(companyParams.SearchTerm)
                .IncludeEmployees(companyParams.IncludeEmployees)
                .ToListAsync();

            var paging = new PagingInfoDto
            {
                CurrentPage = companyParams.PageNumber,
                ItemsPerPage = companyParams.PageSize,
                TotalItems = await _repository.Company.GetAllCompanies(track).CountAsync()
            };

            return (companyDtos: companies, pagingInfoDto: paging);
        }

        public async Task<IEnumerable<CompanyDto>> GetCompaniesByIds(
            IEnumerable<int> ids,
            CompanyParams companyParams,
            bool trackChanges
        ) =>
            await _repository
                .Company
                .GetCompaniesByIds(ids, trackChanges)
                .IncludeEmployees(companyParams.IncludeEmployees)
                .ToListAsync();

        public async Task<CompanyDto> GetCompanyById(
            int id,
            CompanyParams companyParams,
            bool track
        )
        {
            var company = await _repository.Company.GetCompanyById(id, track);

            return company.IncludeEmployees(companyParams.IncludeEmployees);
        }

        public async Task UpdateCompany(int companyId, CompanyForUpdateDto companyForUpdate)
        {
            var company = await _repository.Company.GetCompanyById(companyId, true);

            company.Name = companyForUpdate.Name;
            company.Address = companyForUpdate.Address;
            company.Country = companyForUpdate.Country;
            company.Employees = companyForUpdate
                .Employees
                .Select(
                    e =>
                        new Employee
                        {
                            Name = e.Name,
                            Age = e.Age,
                            Position = e.Position
                        }
                )
                .ToList();

            _repository.Company.UpdateCompany(company);
            await _repository.Save();
        }
    }
}
