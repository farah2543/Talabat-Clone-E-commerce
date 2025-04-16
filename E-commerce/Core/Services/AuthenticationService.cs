using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class AuthenticationService(UserManager<User> _userManager) : IAuthenticationService
    {
        public async Task<UserResultDto> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
                throw new UnauthorizedAccessException();

            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!result)
                throw new UnauthorizedAccessException();


            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);
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

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);

            }



            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);
        }


        private async Task<string> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, user.DisplayName),
                new Claim (ClaimTypes.Email, user.Email),


            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("3138432c4de0a1b8655e93baecb48b99382910fa910690e4e422ab54c83cea713080407a6d59bb7f9f4615916e44ce5f9d6a0ea1f0d6041b95d16653d2c321a4cb83e41544c177372905368baac0c7c04506ef47b813151df32e02a781de56a52c0205201904b05e031848e38d19278b6420cb4ca9e3c63b63abea7a12dcca7415affaf08986a564680f06626273e13c5b20740049e1b86183ff848835409504d9d0a77a99f5ac6218b990505a1bf05d081a94c8bd85fcaa996122bd3a4eeadabb5c03e786fc7fb218180b66ed0c4b2b0e9a2aeb1d62d5bb59a4776126e9344e0c599aa6a88b548700a2dfeff763a9dcef6c0de853e866dab22a21fa7bbe1cd4"));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: "https://localhost:7026/", audience: "my audience"
                , claims: claims, notBefore: DateTime.UtcNow.AddDays(30)
                , signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
