using Employees.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace Employees.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        public Task<bool> IsValidUser(UserForAuthenticationDto userForAuthentication);
        public Task<string> CreateToken();
    }
}
