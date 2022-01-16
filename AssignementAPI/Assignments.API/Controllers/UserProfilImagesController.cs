using Assignments.API.Controllers.Base;
using Assignments.API.Models.Authentification;
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
        public UserProfilImagesController(IUserProfilImageService service, UserIdentity identity, ILogger<UserProfilImagesController> logger) : base(identity, logger)
        {
        }
    }
}
