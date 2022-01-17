using Assignments.API.Controllers.Base;
using Assignments.API.Models.Assignments;
using Assignments.API.Models.Authentification;
using Assignments.API.Models.Authorization;
using Assignments.API.Models.Courses;
using Assignments.API.Models.Search;
using Assignments.API.Services.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class CoursesController : BaseAssignmentController
    {

        private readonly ICourseService Service;

        public CoursesController(ICourseService service, UserIdentity identity, ILogger<CoursesController> logger) : base(identity, logger)
        {
            Service = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Course), 200)]
        public async Task<ActionResult<Course>> Get(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetCourseByIdAsync(id));
            });
        }

        [HttpPost("{id}/assignments")]
        [ProducesResponseType(typeof(PaginationResult<Assignment>), 200)]
        public async Task<ActionResult> GetAllAssignments(int id, [FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetAllAssignmentCourseAsync(id, form));
            });
        }

        [HttpPost("all")]
        [ProducesResponseType(typeof(PaginationResult<Course>), 200)]
        public async Task<ActionResult> GetAll([FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetAllCoursesAsync(form));
            });
        }

        [HttpPost("mine")]
        [ProducesResponseType(typeof(PaginationResult<Course>), 200)]
        public async Task<ActionResult> GetMine([FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetMineCoursesAsync(form));
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(Course), 200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Create([FromBody] CourseFormCreate form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
                {
                    return Ok(await Service.CreateCourseAsync(form));
                }
            );
        }

        [HttpPut]
        [ProducesResponseType(typeof(Course), 200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Update([FromBody] CourseFormUpdate form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
                {
                    return Ok(await Service.UpdateCourseAsync(form));
                }
            );
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Delete(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
                {
                    await Service.DeleteCourseAsync(id);
                    return Ok();
                }
            );
        }
    }
}
