using Assignments.API.Controllers.Base;
using Assignments.Business.Dto.Authentification;
using Assignments.Business.Services.CourseImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseImagesController : BaseAssignmentController
    {
        private readonly ICourseImageService Service;

        public CourseImagesController(ICourseImageService service, UserIdentity identity, ILogger<CourseImagesController> logger) : base(identity, logger)
        {
            Service = service;
        }

        [HttpPost("upload/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(int id, IFormFile file, CancellationToken cancellationToken)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                await Service.UploadFile(id, file, cancellationToken);
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
        [HttpGet("course/{id}")]
        public async Task<ActionResult> CoursePicture(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                var image = await Service.GetPictureOfCourseById(id);
                return File(image.Data, image.Extention);
            });
        }
    }
}