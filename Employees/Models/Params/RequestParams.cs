namespace Employees.Models.Params
{
    public abstract record RequestParams
    {
        const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Min(MaxPageSize, value);
        }

        public string? SearchTerm { get; init; }
        public string? FilterValue { get; init; }
    }
}
