using Employees.Models;
using Employees.Models.Params;

namespace Employees.Extensions
{
    public static class EmployeeQuery
    {
        public static IQueryable<Employee> SortEmployees(
            this IQueryable<Employee> employees,
            EmployeeSortBy? sortBy
        )
        {
            employees = sortBy switch
            {
                EmployeeSortBy.Name => employees.OrderBy(e => e.Name),
                EmployeeSortBy.Age => employees.OrderBy(e => e.Age),
                EmployeeSortBy.Position => employees.OrderBy(e => e.Position),
                _ => employees
            };

            return employees;
        }

        public static IQueryable<Employee> FilterEmployees(
            this IQueryable<Employee> employees,
            EmployeeFilterBy? filterBy,
            string? filterValue,
            int? minAge,
            int? maxAge
        )
        {
            employees = filterBy switch
            {
                EmployeeFilterBy.Name => employees.Where(e => e.Name.Contains(filterValue)),
                EmployeeFilterBy.Age => employees.Where(e => e.Age >= minAge && e.Age <= maxAge),
                EmployeeFilterBy.Position => employees.Where(e => e.Position.Contains(filterValue)),
                _ => employees
            };

            return employees;
        }

        public static IQueryable<Employee> SearchEmployees(
            this IQueryable<Employee> employees,
            string? searchTerm
        )
        {
            if (!string.IsNullOrEmpty(searchTerm))
                return employees.Where(e => e.Name.Contains(searchTerm));

            return employees;
        }
    }
}
