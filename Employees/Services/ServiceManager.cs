using Employees.Interfaces;

namespace Employees.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repository;
        private readonly Lazy<ICompanyService> _company;
        private readonly Lazy<IEmployeeService> _employee;

        public ServiceManager(IRepositoryManager repository)
        {
            _repository = repository;
            _company = new Lazy<ICompanyService>(() => new CompanyService(repository));
            _employee = new Lazy<IEmployeeService>(() => new EmployeeService(repository));
        }

        public ICompanyService Company => _company.Value;
        public IEmployeeService Employee => _employee.Value;
    }
}
