using Assignments.API.Controllers.Base;
using Assignments.API.Models.Authentification;
using Assignments.API.Services.CourseImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseImagessController : BaseAssignmentController
    {
        public CourseImagessController(ICourseImageService service, UserIdentity identity, ILogger<CourseImagessController> logger) : base(identity, logger)
        {
        }
    }
}
