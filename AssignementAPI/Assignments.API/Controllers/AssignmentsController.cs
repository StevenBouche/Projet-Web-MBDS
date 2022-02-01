using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assignments.API.Controllers.Base;
using Assignments.Business.Services.Assignments;
using Assignments.Business.Dto.Authentification;
using Assignments.Business.Dto.Assignments;
using Assignments.Business.Dto.Search;
using Assignments.Business.Dto.WorkSubmits;
using Assignments.Business.Dto.Authorization;
using Assignments.Business.Dto.Search.Assignments;

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

        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(AssignmentDetails), 200)]
        public async Task<ActionResult<Assignment>> GetDetails(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetAssignmentDetailsByIdAsync(id));
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

        [HttpPost("mine")]
        [ProducesResponseType(typeof(PaginationResult<Assignment>), 200)]
        public async Task<ActionResult> GetMine([FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetMineAssignmentsAsync(form));
            });
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(AssignmentsSearchResult), 200)]
        public ActionResult GetSearch([FromBody] AssignmentsSearchForm form)
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(Service.SearchAssignments(form));
            });
        }

        [HttpPost("{id}/works")]
        [ProducesResponseType(typeof(PaginationResult<WorkSubmit>), 200)]
        public async Task<ActionResult> GetAllAssignments(int id, [FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetAllWorksAssignmentAsync(id, form));
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(Assignment), 200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Create([FromBody] AssignmentForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.CreateAssignmentAsync(form));
            });
        }

        [HttpPut]
        [ProducesResponseType(typeof(Assignment), 200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Update([FromBody] AssignmentForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.UpdateAssignmentAsync(form));
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Delete(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                await Service.DeleteAssignmentAsync(id);
                return Ok();
            });
        }

        [HttpPut("open/{id}")]
        [ProducesResponseType(typeof(Assignment), 200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Open(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.OpenAssignmentAsync(id));
            });
        }

        [HttpPut("close/{id}")]
        [ProducesResponseType(typeof(Assignment), 200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        public async Task<ActionResult> Close(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.CloseAssignmentAsync(id));
            });
        }
    }
}