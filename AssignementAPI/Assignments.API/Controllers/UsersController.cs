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
        [Authorize(Roles = AuthorizationConstants.ADMIN)]
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

        /*  [HttpDelete("{id}")]
          public ActionResult Delete(string id)
          {
              this.Manager.DeleteAccount(id);
              return Ok();
          }*/

        /*   [HttpGet("identity")]
           public ActionResult<AccountView> MyIdentity()
           {
               return Ok(this.Manager.GetAccountById(this.Identity.ID).ToAccountView());
           }

           [HttpGet("{id}")]
           public ActionResult<AccountView> Get(string id)
           {
               if (this.Identity.Role.Equals("ADMIN"))
                   return Ok(this.Manager.GetAccountById(id).ToAccountView());
               else return Unauthorized();
           }

           [HttpGet]
           public ActionResult<List<AccountView>> Get()
           {
               if (this.Identity.Role.Equals("ADMIN"))
                   return Ok(this.Manager.GetAllAccount().Select(account => account.ToAccountView()).ToList());
               else return Unauthorized();
           }

           [HttpPut]
           public AccountView Put([FromBody] AccountView element)
           {
               element.ID = this.Identity.ID;
               this.Manager.UpdateAccountFromView(element);
               return element;
           }*/
    }
}