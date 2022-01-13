using Assignments.API.Controllers.Base;
using Assignments.API.Models.Api;
using Assignments.API.Models.Authorization;
using Assignments.API.Models.Search;
using Assignments.API.Models.WorkSubmits;
using Assignments.API.Services.Authorization;
using Assignments.API.Services.WorkSubmits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class WorkSubmitsController : BaseAssignmentController
    {
        private readonly IWorkSubmitService Service;

        protected WorkSubmitsController(IWorkSubmitService service, IAuthorizeService authorizationService, ILogger<WorkSubmitsController> logger) : base(authorizationService, logger)
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

        [HttpPost("my")]
        [Authorize(AuthorizationConstants.AuthorizationPolicy_Student)]
        [ProducesResponseType(typeof(PaginationResult<WorkSubmit>), 200)]
        [ProducesResponseType(typeof(PaginationResult<ApiErrorResponse>), 403)]
        public async Task<ActionResult> GetMy([FromBody] PaginationForm form)
        {
            return await TryExecuteWithAuthorizationAsync<ActionResult>(async (identity) =>
            {
                return Ok(await Service.GetMyWorkSubmitsAsync(form, identity));
            });
        }

        [HttpPost]
        [Authorize(AuthorizationConstants.AuthorizationPolicy_Student)]
        [ProducesResponseType(typeof(WorkSubmit), 200)]
        [ProducesResponseType(typeof(PaginationResult<ApiErrorResponse>), 403)]
        public async Task<ActionResult> Create([FromBody] WorkSubmitStudentForm form)
        {
            return await TryExecuteWithAuthorizationAsync<ActionResult>(async (identity) =>
            {
                return Ok(await Service.CreateWorkSubmitAsync(form, identity));
            });
        }

        [HttpPut]
        [Authorize(AuthorizationConstants.AuthorizationPolicy_Student)]
        [ProducesResponseType(typeof(WorkSubmit), 200)]
        [ProducesResponseType(typeof(PaginationResult<ApiErrorResponse>), 403)]
        public async Task<ActionResult> Update([FromBody] WorkSubmitStudentForm form)
        {
            return await TryExecuteWithAuthorizationAsync<ActionResult>(async (identity) =>
            {
                return Ok(await Service.UpdateWorkSubmitAsync(form, identity));
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [Authorize(AuthorizationConstants.AuthorizationPolicy_Student)]
        [Authorize(AuthorizationConstants.AuthorizationPolicy_Admin)]
        [ProducesResponseType(typeof(PaginationResult<ApiErrorResponse>), 403)]
        public async Task<ActionResult> Delete(int id)
        {
            return await TryExecuteWithAuthorizationAsync<ActionResult>(async (identity) =>
            {
                await Service.DeleteWorkSubmitAsync(id, identity);
                return Ok();
            });
        }
    }
}
