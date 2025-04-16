using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs;

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





    }
}
