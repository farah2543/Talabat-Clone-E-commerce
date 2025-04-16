using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared;
using Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> options) : IAuthenticationService
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
            var jwtOptions = options.Value;

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
                Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer:jwtOptions.Issuer , audience: jwtOptions.Audience
                , claims: claims, expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDays)
                , signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
