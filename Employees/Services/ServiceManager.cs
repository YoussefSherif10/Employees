using Employees.Interfaces;
using Employees.Models;
using Microsoft.AspNetCore.Identity;

namespace Employees.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _company;
        private readonly Lazy<IEmployeeService> _employee;
        private readonly Lazy<IAuthenticationService> _authentication;

        public ServiceManager(
            IRepositoryManager repository,
            UserManager<User> userManager,
            IConfiguration configuration
        )
        {
            _company = new Lazy<ICompanyService>(() => new CompanyService(repository));
            _employee = new Lazy<IEmployeeService>(() => new EmployeeService(repository));
            _authentication = new Lazy<IAuthenticationService>(
                () => new AuthenticationService(configuration, userManager)
            );
        }

        public ICompanyService Company => _company.Value;
        public IEmployeeService Employee => _employee.Value;
        public IAuthenticationService Authentication => _authentication.Value;
    }
}
