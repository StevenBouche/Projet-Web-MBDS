using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assignments.API.Controllers.Base;
using Assignments.API.Services.Assignments;
using Assignments.API.Models.Assignments;
using Assignments.API.Models.Search;
using Assignments.API.Models.Api;
using Assignments.API.Models.Authentification;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class AssignmentsController : BaseAssignmentController
    {
        private readonly IAssignmentService Service;

        public AssignmentsController(IAssignmentService service, UserIdentity identity, ILogger<AssignmentsController> logger) : base(identity, logger)
        {
            Service = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Assignment), 200)]
        public async Task<ActionResult<Assignment>> Get(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetAssignmentByIdAsync(id));
            });
        }

        [HttpPost("all")]
        [ProducesResponseType(typeof(PaginationResult<Assignment>), 200)]
        public async Task<ActionResult> GetAll([FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetAllAssignmentsAsync(form));
            });
        }

        [HttpPost("my")]
        [ProducesResponseType(typeof(PaginationResult<Assignment>), 200)]
        [ProducesResponseType(typeof(PaginationResult<ApiErrorResponse>), 403)]
        public async Task<ActionResult> GetMy([FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetMyAssignmentsAsync(form, Identity));
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(Assignment), 200)]
        [ProducesResponseType(typeof(PaginationResult<ApiErrorResponse>), 403)]
        public async Task<ActionResult> Create([FromBody] AssignmentForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.CreateAssignmentAsync(form, Identity));
            });
        }

        [HttpPut]
        [ProducesResponseType(typeof(Assignment), 200)]
        [ProducesResponseType(typeof(PaginationResult<ApiErrorResponse>), 403)]
        public async Task<ActionResult> Update([FromBody] AssignmentForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.UpdateAssignmentAsync(form, Identity));
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(PaginationResult<ApiErrorResponse>), 403)]
        public async Task<ActionResult> Delete(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                await Service.DeleteAssignmentAsync(id, Identity);
                return Ok();
            });
        }
    }
}
