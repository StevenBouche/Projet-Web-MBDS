using Assignments.API.Controllers.Base;
using Assignments.API.Services.UserProfilImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfilImagesController : BaseAssignmentController
    {
        public UserProfilImagesController(IUserProfilImageService service, ILogger<UserProfilImagesController> logger) : base(logger)
        {
        }
    }
}
