using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Employees.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Address), IsUnique = true)]
    [Index(nameof(Country))]
    public class Company
    {
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Company name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company address is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
        public string Address { get; set; } = string.Empty;

        [MaxLength(60, ErrorMessage = "Maximum length for the Country is 60 characters")]
        public string? Country { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
