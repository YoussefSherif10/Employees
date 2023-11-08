using Employees.Interfaces;
using Employees.Models;

namespace Employees.Services
{
    public sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;

        public CompanyService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies(bool track) =>
            await _repository.Company.GetAllCompanies(track);
    }
}
