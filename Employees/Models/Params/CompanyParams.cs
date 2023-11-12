namespace Employees.Models.Params
{
    public record CompanyParams : RequestParams
    {
        public CompanyFilterBy? FilterBy { get; init; }
        public CompanySortBy? SortBy { get; init; }
        public int MinEmployees { get; init; } = 0;
        public bool IncludeEmployees { get; set; } = false;
    }
}
