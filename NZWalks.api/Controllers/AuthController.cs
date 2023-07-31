using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repository;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO requestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = requestDTO.UserName,
                Email = requestDTO.UserName
            };
            var identityResult = await userManager.CreateAsync(identityUser, requestDTO.Password);

            if (identityResult.Succeeded)
            {
                //Add roles
                if (requestDTO.Roles != null && requestDTO.Roles.Any())
                {
                   identityResult= await userManager.AddToRolesAsync(identityUser, requestDTO.Roles);
                }

                if(identityResult.Succeeded)
                {
                    return Ok("User is successfully added");
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO )
        {
            var userName = await userManager.FindByEmailAsync(loginRequestDTO.UserName);

            if (userName != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(userName, loginRequestDTO.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(userName);
                    if(roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(userName, roles.ToList());
                        return Ok("Success Token: "+jwtToken);
                    }
                    
                }
            }

            return BadRequest("UserName or Passowrd is incorrect");
        }
    }
}
