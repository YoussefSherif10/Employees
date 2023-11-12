using Employees.Models;
using Employees.Models.Params;

namespace Employees.Extensions
{
    public static class CompanyQuery
    {
        public static IQueryable<Company> SortCompanies(
            this IQueryable<Company> query,
            CompanySortBy? sortBy
        )
        {
            query = sortBy switch
            {
                CompanySortBy.Name => query.OrderBy(c => c.Name),
                CompanySortBy.Country => query.OrderBy(c => c.Country),
                CompanySortBy.Address => query.OrderBy(c => c.Address),
                CompanySortBy.NumberOfEmployees => query.OrderBy(c => c.Employees.Count),
                _ => query.OrderBy(c => c.CompanyId)
            };

            return query;
        }

        public static IQueryable<Company> FilterCompanies(
            this IQueryable<Company> query,
            CompanyFilterBy? filterBy,
            string? filterValue,
            int? minEmployees
        )
        {
            query = filterBy switch
            {
                CompanyFilterBy.Name => query.Where(c => c.Name.Contains(filterValue)),
                CompanyFilterBy.Country => query.Where(c => c.Country.Contains(filterValue)),
                CompanyFilterBy.Address => query.Where(c => c.Address.Contains(filterValue)),
                CompanyFilterBy.NumberOfEmployees
                    => query.Where(c => c.Employees.Count >= minEmployees),
                _ => query
            };

            return query;
        }

        public static IQueryable<Company> SearchCompanies(
            this IQueryable<Company> query,
            string? searchTerm
        )
        {
            if (!string.IsNullOrEmpty(searchTerm))
                return query.Where(x => x.Name.Contains(searchTerm));

            return query;
        }
    }
}
