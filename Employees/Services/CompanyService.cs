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

        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            var entity = new Company
            {
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };

            _repository.Company.CreateCompany(entity);
            _repository.Save();

            return new CompanyDto(
                entity.CompanyId,
                entity.Name,
                string.Join(", ", entity.Address, entity.Country)
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
