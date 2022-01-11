using AssignmentAPI.Services.CourseImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Controllers
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
