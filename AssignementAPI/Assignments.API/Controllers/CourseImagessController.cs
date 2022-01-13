using Assignments.API.Controllers.Base;
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
        public CourseImagessController(ICourseImageService service, ILogger<CourseImagessController> logger) : base(logger)
        {
        }
    }
}
