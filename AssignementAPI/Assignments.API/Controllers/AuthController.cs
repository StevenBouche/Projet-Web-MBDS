using Assignments.API.Controllers.Base;
using Assignments.Business.Dto.Api;
using Assignments.Business.Dto.Authentification;
using Assignments.Business.Dto.Authentification.Tokens;
using Assignments.Business.Exceptions.Authentification;
using Assignments.Business.Services.Authentification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : BaseAssignmentController
    {
        private readonly IAuthentificationService SecurityService;

        public AuthController(IAuthentificationService securityService, UserIdentity identity, ILogger<AuthController> logger) : base(identity, logger)
        {
            SecurityService = securityService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(typeof(LoginResult), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        public async Task<ActionResult> Login([FromBody] LoginForm account)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await SecurityService.LoginAsync(account));
            });
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(LoginResult), 200)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshToken token)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await SecurityService.RefreshLoginAsync(token));
            });
        }

        [Authorize]
        [HttpDelete("revoke")]
        public async Task<ActionResult> Logout()
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                await SecurityService.LogoutAsync(Identity);
                return Ok();
            });
        }

        protected override ActionResult HandleException(Exception exception)
        {
            switch (exception)
            {
                case AuthentificationException _:
                    return LogInfoAndReturn(exception, HttpStatusCode.BadRequest);

                default:
                    return base.HandleException(exception);
            }
        }
    }
}