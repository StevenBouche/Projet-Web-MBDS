using AssignmentAPI.Services.Assignment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentsController : BaseAssignmentController
    {
        public AssignmentsController(IAssignmentService service, ILogger<AssignmentsController> logger) : base(logger)
        {
        }
    }
}
