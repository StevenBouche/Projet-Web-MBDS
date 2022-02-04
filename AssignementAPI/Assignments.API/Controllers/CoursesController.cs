using Assignments.API.Controllers.Base;
using Assignments.Business.Dto.Assignments;
using Assignments.Business.Dto.Authentification;
using Assignments.Business.Dto.Authorization;
using Assignments.Business.Dto.Courses;
using Assignments.Business.Dto.Search;
using Assignments.Business.Dto.Search.Courses;
using Assignments.Business.Dto.Tree;
using Assignments.Business.Services.Courses;
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

        [HttpGet("{id}/assignments")]
        [ProducesResponseType(typeof(IList<Assignment>), 200)]
        public ActionResult GetAllAssignments(int id)
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(Service.GetAllAssignmentCourse(id));
            });
        }

        [HttpPost("all")]
        [ProducesResponseType(typeof(CoursesPaginationResult), 200)]
        public ActionResult GetAll([FromBody] CoursesPaginationForm form)
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(Service.GetAllCourses(form));
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

        [HttpGet("mine/tree")]
        [ProducesResponseType(typeof(IList<CourseTreeNode>), 200)]
        public ActionResult GetMineTree()
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(Service.GetMineTreeCourses());
            });
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(CoursesSearchResult), 200)]
        public ActionResult GetSearch([FromBody] CoursesSearchForm form)
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(Service.SearchCourses(form));
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