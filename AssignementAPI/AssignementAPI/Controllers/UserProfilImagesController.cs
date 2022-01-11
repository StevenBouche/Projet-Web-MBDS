using AssignmentAPI.Services.UserProfilImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Controllers
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
