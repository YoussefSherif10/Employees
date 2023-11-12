namespace Employees.Extensions
{
    public static class QueryBase
    {
        public static IQueryable<T> Pagination<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize
        ) => query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}
