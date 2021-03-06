using Assignments.API.Controllers.Base;
using Assignments.Business.Dto.Authentification;
using Assignments.Business.Dto.Authorization;
using Assignments.Business.Dto.Search;
using Assignments.Business.Dto.Search.Work;
using Assignments.Business.Dto.WorkSubmits;
using Assignments.Business.Services.WorkSubmits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AuthorizationConstants.ALL)]
    [Produces("application/json")]
    public class WorkSubmitsController : BaseAssignmentController
    {
        private readonly IWorkSubmitService Service;

        public WorkSubmitsController(IWorkSubmitService service, UserIdentity identity, ILogger<WorkSubmitsController> logger) : base(identity, logger)
        {
            Service = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkSubmit), 200)]
        public async Task<ActionResult<WorkSubmit>> Get(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetWorkSubmitByIdAsync(id));
            });
        }

        [HttpPost("all")]
        [ProducesResponseType(typeof(PaginationResult<WorkSubmit>), 200)]
        public async Task<ActionResult> GetAll([FromBody] PaginationForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.GetAllWorkSubmitsAsync(form));
            });
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(WorkPaginationResult), 200)]
        public ActionResult Search([FromBody] WorkPaginationForm form)
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(Service.SearchWorkSubmitsAsync(form));
            });
        }

        [HttpPost]
        [Authorize(Roles = AuthorizationConstants.STUDENT)]
        [ProducesResponseType(typeof(WorkSubmit), 200)]
        public async Task<ActionResult> Create([FromBody] WorkSubmitStudentForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.CreateWorkSubmitAsync(form));
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = AuthorizationConstants.STUDENT_ADMIN)]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Delete(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                await Service.DeleteWorkSubmitAsync(id);
                return Ok();
            });
        }

        [HttpPut("submit-evaluation")]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR)]
        [ProducesResponseType(typeof(WorkSubmit), 200)]
        public async Task<ActionResult> SubmitEvaluation([FromBody] WorkSubmitActionForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.EvaluateWorkSubmitAsync(form));
            });
        }

        [HttpPut("submit-work")]
        [Authorize(Roles = AuthorizationConstants.STUDENT)]
        [ProducesResponseType(typeof(WorkSubmit), 200)]
        public async Task<ActionResult> SubmitWork([FromBody] WorkSubmitActionForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.SubmitWorkSubmitAsync(form));
            });
        }

        [HttpPut("work")]
        [Authorize(Roles = AuthorizationConstants.STUDENT_ADMIN)]
        [ProducesResponseType(typeof(WorkSubmit), 200)]
        public async Task<ActionResult> UpdateWork([FromBody] WorkSubmitStudentForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.UpdateWorkSubmitAsync(form));
            });
        }

        [HttpPut("evaluation")]
        [ProducesResponseType(200)]
        [Authorize(Roles = AuthorizationConstants.PROFESSOR_ADMIN)]
        [ProducesResponseType(typeof(WorkSubmit), 200)]
        public async Task<ActionResult> UpdateEvaluation([FromBody] WorkSubmitProfessorForm form)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await Service.UpdateWorkSubmitAsync(form));
            });
        }
    }
}