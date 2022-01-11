using AssignmentAPI.Models.Authentification;
using AssignmentAPI.Models.Authentification.Security;
using AssignmentAPI.Models.Authentification.View;
using AssignmentAPI.Services.Authentification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseAssignmentController
    {
        private readonly ISecurityService SecurityService;

        public AuthController(ISecurityService securityService, ILogger<AuthController> logger) : base(logger)
        {
            SecurityService = securityService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login([FromBody] LoginView account)
        {
            var loginResult = await SecurityService.LoginAsync(account);

            return loginResult.Status switch
            {
                LoginResultCode.SUCCESS => Ok(loginResult),
                _ => BadRequest(loginResult),
            };
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResult>> RefreshToken([FromBody] RefreshToken token)
        {
            var loginResult = await SecurityService.RefreshLoginAsync(token);

            return loginResult.Status switch
            {
                LoginResultCode.SUCCESS => Ok(loginResult),
                _ => BadRequest(loginResult),
            };
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<ActionResult> Logout()
        {
            await SecurityService.LogoutAsync(Identity);
            return Ok();
        }
    }
}
