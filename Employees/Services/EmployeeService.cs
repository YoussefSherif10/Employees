using Employees.Interfaces;
using Employees.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;

        public EmployeeService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees(
            int companyId,
            bool trackChanges
        ) =>
            await _repository
                .Employee
                .GetAllEmployees(companyId, trackChanges)
                .Select(e => new EmployeeDto(e.EmployeeId, e.Name, e.Position, e.Age))
                .ToListAsync();

        public async Task<EmployeeDto> GetEmployeeById(int companyId, int id, bool track)
        {
            var employee = await _repository.Employee.GetEmployeeById(companyId, id, track);
            return new EmployeeDto(
                employee.EmployeeId,
                employee.Name,
                employee.Position,
                employee.Age
            );
        }
    }
}
