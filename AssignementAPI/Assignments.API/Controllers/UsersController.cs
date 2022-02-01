using Assignments.API.Controllers.Base;
using Assignments.Business.Dto.Authentification;
using Assignments.Business.Dto.Authorization;
using Assignments.Business.Dto.Users;
using Assignments.Business.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class UsersController : BaseAssignmentController
    {
        private readonly IUserService UserService;

        public UsersController(IUserService service, UserIdentity identity, ILogger<UsersController> logger) : base(identity, logger)
        {
            UserService = service;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult> Create([FromBody] UserForm element)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await UserService.CreateUserAsync(element));
            });
        }

        [HttpGet("identity")]
        [ProducesResponseType(typeof(UserIdentity), 200)]
        public ActionResult<UserIdentity> MyIdentity()
        {
            return Ok(Identity);
        }

        [HttpPut]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<ActionResult> Update([FromBody] UserForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await UserService.UpdateUserFromView(form));
            }
            );
        }
    }
}