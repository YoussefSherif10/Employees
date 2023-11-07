using Employees.Data;
using Employees.Interfaces;

namespace Employees.Services
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _repositoryContext;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        public RepositoryManager(AppDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(
                () => new CompanyRepository(_repositoryContext)
            );
            _employeeRepository = new Lazy<IEmployeeRepository>(
                () => new EmployeeRepository(_repositoryContext)
            );
        }

        public ICompanyRepository Company => _companyRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;

        public async void Save() => await _repositoryContext.SaveChangesAsync();
    }
}
