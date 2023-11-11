using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Employees.Models.DTO
{
    public abstract record CompanyForManipulationDto
    {
        [Required(ErrorMessage = "Company name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company address is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
        public string Address { get; set; } = string.Empty;

        [MaxLength(60, ErrorMessage = "Maximum length for the Country is 60 characters")]
        public string? Country { get; set; }

        [ValidateNever]
        public ICollection<Employee>? Employees { get; set; }
    }
}
