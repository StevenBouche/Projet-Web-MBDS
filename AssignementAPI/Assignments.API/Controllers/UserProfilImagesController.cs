using Assignments.API.Controllers.Base;
using Assignments.Business.Dto.Authentification;
using Assignments.Business.Services.UserProfilImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfilImagesController : BaseAssignmentController
    {
        private readonly IUserProfilImageService Service;

        public UserProfilImagesController(IUserProfilImageService service, UserIdentity identity, ILogger<UserProfilImagesController> logger) : base(identity, logger)
        {
            Service = service;
        }

        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                await Service.UploadFile(file, cancellationToken);
                return Ok();
            });
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> Picture(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                var image = await Service.GetPictureById(id);
                return File(image.Data, image.Extention);
            });
        }

        [AllowAnonymous]
        [HttpGet("user/{id}")]
        public async Task<ActionResult> UserPicture(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                var image = await Service.GetPictureByUserId(id);
                return File(image.Data, image.Extention);
            });
        }
    }
}