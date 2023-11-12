using Employees.Extensions;
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
        )> GetAllEmployees(int companyId, EmployeeParams employeeParams, bool trackChanges)
        {
            if (!employeeParams.ValidRange)
                throw new BadHttpRequestException("the entered range for the ages is not valid");

            var employees = _repository
                .Employee
                .GetAllEmployees(companyId, trackChanges)
                .SortEmployees(employeeParams.SortBy)
                .Pagination(employeeParams.PageNumber, employeeParams.PageSize)
                .FilterEmployees(
                    employeeParams.FilterBy,
                    employeeParams.FilterValue,
                    employeeParams.MinAge,
                    employeeParams.MaxAge
                )
                .SearchEmployees(employeeParams.SearchTerm);

            return (
                employeeDtos: await employees
                    .Select(e => new EmployeeDto(e.EmployeeId, e.Name, e.Position, e.Age))
                    .ToListAsync(),
                pagingInfoDto: new PagingInfoDto
                {
                    CurrentPage = employeeParams.PageNumber,
                    ItemsPerPage = employeeParams.PageSize,
                    TotalItems = await _repository
                        .Employee
                        .GetAllEmployees(companyId, trackChanges)
                        .CountAsync()
                }
            );
        }

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
