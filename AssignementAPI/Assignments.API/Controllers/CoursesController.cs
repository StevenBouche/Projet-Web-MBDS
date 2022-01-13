using Assignments.API.Controllers.Base;
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

        public CoursesController(ICourseService service, ILogger<CoursesController> logger) : base(logger)
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

        [HttpPost("my")]
        [ProducesResponseType(typeof(PaginationResult<Course>), 200)]
        public async Task<ActionResult> GetMy([FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetMyCoursesAsync(form, Identity));
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(Course), 200)]
        public async Task<ActionResult> Create([FromBody] CourseForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                if (!ModelState.IsValid)
                {
                    //return ();
                }
                return Ok(await Service.CreateCourseAsync(form, Identity));
            });
        }

        [HttpPut]
        [ProducesResponseType(typeof(Course), 200)]
        public async Task<ActionResult> Update([FromBody] CourseForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.UpdateCourseAsync(form, Identity));
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Delete(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                await Service.DeleteCourseAsync(id, Identity);
                return Ok();
            });
        }
    }
}
