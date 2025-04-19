using Shared.DTOs;
using Shared.OrderModels;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> Login(LoginDTO loginDTO);

        public Task<UserResultDto> Register(RegisterDTO registerDTO);

        Task<UserResultDto> GetUserByEmail(string email);

        Task<bool> CheckIfEmailExists(string email);

        Task<AddressDTO> UpdateUserAddress(AddressDTO addressDto,string email);

        Task<AddressDTO> GetUserAddress(string email);
    }
     
}
