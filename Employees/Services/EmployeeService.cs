using Employees.Interfaces;
using Employees.Models;
using Employees.Models.DTO;
using Employees.Models.Params;
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

        public EmployeeDto CreateEmployee(int companyId, EmployeeForCreationDto employee)
        {
            var entity = new Employee
            {
                CompanyId = companyId,
                Name = employee.Name,
                Position = employee.Position,
                Age = employee.Age
            };

            _repository.Employee.CreateEmployee(entity);
            _repository.Save();

            return new EmployeeDto(entity.EmployeeId, entity.Name, entity.Position, entity.Age);
        }

        public async Task DeleteEmployee(int companyId, int id)
        {
            var employee = new Employee { EmployeeId = id };
            _repository.Employee.DeleteEmployee(employee);
            await _repository.Save();
        }

        public async Task<(
            IEnumerable<EmployeeDto> employeeDtos,
            PagingInfoDto pagingInfoDto
        )> GetAllEmployees(int companyId, EmployeeParams employeeParams, bool trackChanges) =>
            (
                employeeDtos: await _repository
                    .Employee
                    .GetAllEmployees(companyId, trackChanges)
                    .OrderBy(e => e.Name)
                    .Skip((employeeParams.PageNumber - 1) * employeeParams.PageSize)
                    .Take(employeeParams.PageSize)
                    .Select(e => new EmployeeDto(e.EmployeeId, e.Name, e.Position, e.Age))
                    .ToListAsync(),
                pagingInfoDto: new PagingInfoDto(
                    employeeParams.PageNumber,
                    employeeParams.PageSize,
                    await _repository.Employee.GetAllEmployees(companyId, trackChanges).CountAsync()
                )
            );

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

        public async Task UpdateEmployee(int companyId, int id, EmployeeForUpdateDto employee)
        {
            var emp = await _repository.Employee.GetEmployeeById(companyId, id, true);
            emp.Name = employee.Name;
            emp.Position = employee.Position;
            emp.Age = employee.Age;
            _repository.Employee.UpdateEmployee(emp);
            await _repository.Save();
        }
    }
}
