using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using Shared.DTOs;

namespace Services
{
    public class AuthenticationService (UserManager<User> _userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
                throw new UnauthorizedAccessException();

            // Verify password
            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!result)
                throw new UnauthorizedAccessException();

           
            return new UserResultDto(user.DisplayName, "Token", user.Email);
        }

        public Task<UserResultDto> Register(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
