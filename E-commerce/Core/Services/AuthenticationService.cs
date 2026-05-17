using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared;
using Shared.DTOs;
using Shared.OrderModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> options,IMapper mapper) : IAuthenticationService
    {
  
        public async Task<bool> CheckIfEmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null; // Returns true if email exists, false if not
        }

        public async Task<AddressDTO> GetUserAddress(string email)
        {
            var user = await _userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException( email);

            return mapper.Map<AddressDTO>(user.Address);
        }

        public async Task<UserResultDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException(email);

            return new UserResultDto(user.DisplayName,
                await CreateTokenAsync(user),user.Email);
        }

        public async Task<AddressDTO> UpdateUserAddress(AddressDTO addressDto, string email)
        {

            var user = await _userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);

            if (user.Address != null)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.street = addressDto.street;
            }
            else
            {
                var address = mapper.Map<Address>(addressDto);
                user.Address = address;
            }

            await _userManager.UpdateAsync(user);
            return mapper.Map<AddressDTO>(user.Address);


        }


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
