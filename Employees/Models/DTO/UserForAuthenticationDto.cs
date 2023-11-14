using System.ComponentModel.DataAnnotations;

namespace Employees.Models.DTO
{
    public record UserForAuthenticationDto(
        [Required(ErrorMessage = "UserName is required")] string UserName,
        [Required(ErrorMessage = "password is required")] string Password
    );
}
