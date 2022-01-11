using AssignmentAPI.Services.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : BaseAssignmentController
    {
        public CoursesController(ICourseService service, ILogger<CoursesController> logger) : base(logger)
        {
        }

    }
}
