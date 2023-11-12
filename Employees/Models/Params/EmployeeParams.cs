namespace Employees.Models.Params
{
    public record EmployeeParams : RequestParams
    {
        public byte MinAge { get; init; } = 0;
        public byte MaxAge { get; init; } = byte.MaxValue;
        public EmployeeFilterBy? FilterBy { get; init; }
        public EmployeeSortBy? SortBy { get; init; }

        public bool ValidRange => MaxAge > MinAge;
    }
}
