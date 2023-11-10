using Employees.Interfaces;
using Employees.Models;
using Employees.Models.DTO;
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

        public async Task<IEnumerable<CompanyDto>> GetAllCompanies(bool track)
        {
            var companies = await _repository
                .Company
                .GetAllCompanies(track)
                .Select(
                    c =>
                        new CompanyDto(c.CompanyId, c.Name, string.Join(", ", c.Address, c.Country))
                )
                .ToListAsync();

            return companies;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompaniesByIds(
            IEnumerable<int> ids,
            bool trackChanges
        ) =>
            await _repository
                .Company
                .GetCompaniesByIds(ids, trackChanges)
                .Select(
                    c =>
                        new CompanyDto(c.CompanyId, c.Name, string.Join(", ", c.Address, c.Country))
                )
                .ToListAsync();

        public async Task<CompanyDto> GetCompanyById(int id, bool track)
        {
            var company = await _repository.Company.GetCompanyById(id, track);
            return new CompanyDto(
                company.CompanyId,
                company.Name,
                string.Join(", ", company.Address, company.Country)
            );
        }
    }
}
