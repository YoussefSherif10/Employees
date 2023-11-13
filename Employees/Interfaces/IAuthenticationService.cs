using Employees.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace Employees.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    }
}
