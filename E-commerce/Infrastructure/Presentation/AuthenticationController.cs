using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs;
using Shared.OrderModels;
using System.Security.Claims;

namespace Presentation
{
    public class AuthenticationController (IServiceManager serviceManager) :ApiController
    {

        [HttpPost("Login")]

        public async Task<ActionResult<UserResultDto>> Login(LoginDTO loginDTO)
        {
            var result = await serviceManager.AuthenticationService.Login(loginDTO);
            
            return Ok(result);
        }


        [HttpPost("Register")]

        public async Task<ActionResult<UserResultDto>> Register(RegisterDTO registerDTO)
        {
            var result = await serviceManager.AuthenticationService.Register(registerDTO);

            return Ok(result);
        }

        [HttpGet("EmailExist")]

        public async Task<ActionResult <bool>> CheckEmailExist(string email)
        {
            return Ok(await serviceManager.AuthenticationService.CheckIfEmailExists(email));

        }

        [Authorize]
        [HttpGet]

        public async Task<ActionResult <UserResultDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.AuthenticationService.GetUserByEmail(email) ;

            return Ok(result);  
        }

        [Authorize]
        [HttpGet ("Address")]

        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthenticationService.GetUserAddress(email) ; 
            
            return Ok(result);
        }

        [Authorize]
        [HttpPut("Address")]

        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthenticationService.UpdateUserAddress(address,email);

            return Ok(result);
        }

        










    }
}
