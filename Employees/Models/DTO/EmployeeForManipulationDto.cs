using System.ComponentModel.DataAnnotations;

namespace Employees.Models.DTO
{
    public abstract record EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "the name field is required")]
        [MaxLength(30, ErrorMessage = "the name must be less than 30 characters")]
        public string Name { get; init; } = string.Empty;

        [Range(10, 65, ErrorMessage = "the age must be between 10 and 65")]
        public int Age { get; init; }

        [Required(ErrorMessage = "Employee position is a required field")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters")]
        public string Position { get; init; } = string.Empty;
    }
}
