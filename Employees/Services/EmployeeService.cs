using Employees.Interfaces;

namespace Employees.Services
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;

        public EmployeeService(IRepositoryManager repository)
        {
            _repository = repository;
        }
    }
}
