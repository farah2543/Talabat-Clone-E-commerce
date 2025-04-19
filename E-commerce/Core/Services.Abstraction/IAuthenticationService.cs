using Shared.DTOs;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> Login(LoginDTO loginDTO);

        public Task<UserResultDto> Register(RegisterDTO registerDTO);
    }
     
}
