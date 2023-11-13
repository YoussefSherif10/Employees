using System.ComponentModel.DataAnnotations;

namespace Employees.Models.DTO
{
    public record UserForRegistrationDto(
        string? FirstName,
        string? LastName,
        [Required(ErrorMessage = "UserName is required")] string UserName,
        [Required(ErrorMessage = "password is required")] string Password,
        string? Email,
        string? PhoneNumber,
        ICollection<string>? Roles
    );
}
