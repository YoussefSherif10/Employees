using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Employees.Models
{
    [Index(nameof(Name))]
    [Index(nameof(Age))]
    [Index(nameof(Position))]
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Employee position is a required field")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "Employee age is a required field")]
        public int Age { get; set; }

        public int CompanyId { get; set; }
    }
}
