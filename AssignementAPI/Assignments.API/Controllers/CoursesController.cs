using Assignments.API.Configurations.Authorization;
using Assignments.API.Controllers.Base;
using Assignments.API.Models.Api;
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

        [HttpPost("all")]
        [ProducesResponseType(typeof(PaginationResult<Course>), 200)]
        public async Task<ActionResult> GetAll([FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetAllCoursesAsync(form));
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(Course), 200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Create([FromBody] CourseForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
                {
                   /* if (!ModelState.IsValid)
                    {
                        return ();
                    }*/
                    return Ok(await Service.CreateCourseAsync(form));
                }
            );
        }

        [HttpPut]
        [ProducesResponseType(typeof(Course), 200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Update([FromBody] CourseForm form)
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
