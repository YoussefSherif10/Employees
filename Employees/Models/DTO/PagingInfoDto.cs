namespace Employees.Models.DTO
{
    public record PagingInfoDto
    {
        public int CurrentPage { get; init; }
        public int ItemsPerPage { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    };
}
