using Domain.Entities;
using Domain.Exceptions;
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

        public async Task<UserResultDto> Register(RegisterDTO registerDTO)
        {
            var user = new User()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.PhoneNumber,

            };
            
            var result =  await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
                
            }



            return new UserResultDto(user.DisplayName, "Token", user.Email);
        }
    }
}
