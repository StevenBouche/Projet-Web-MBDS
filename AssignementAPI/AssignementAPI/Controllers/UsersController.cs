using AssignmentAPI.Models.User;
using AssignmentAPI.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseAssignmentController
    {
        private readonly IUserService UserService;
        public UsersController(IUserService service, ILogger<UsersController> logger) : base(logger)
        {
            UserService = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserView>> Create([FromBody] RegisterView element)
        {
            var result = await UserService.CreateUserAsync(element);
            return Ok(result);
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

        [AllowAnonymous]
        [HttpGet("picture/{id}")]
        public ActionResult<string> UserPicture(string id)
        {
            var img = this.Manager.GetPictureUser(id);
            var items = img.Split(new char[] { ',', ':', ';' });
            var type = items[1];
            var image = items[3];
            byte[] b = Convert.FromBase64String(image);
            return new FileContentResult(b, type);
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
