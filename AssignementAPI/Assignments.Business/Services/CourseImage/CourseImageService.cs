using Assignments.Business.Dto.Authentification;
using Assignments.Business.Dto.Image;
using Assignments.Business.Exceptions.Business;
using Assignments.Business.Services.Base;
using Assignments.Business.Services.Courses;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.CourseImage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Assignments.Business.Services.CourseImage
{
    public class CourseImageService : BaseService<CourseImageEntity, ICourseImageRepository>, ICourseImageService
    {
        private readonly UserIdentity Identity;
        private readonly ICourseService CourseService;

        public CourseImageService(ICourseImageRepository repository, ICourseService courseService, UserIdentity identity, ILogger<CourseImageService> logger) : base(repository, logger)
        {
            Identity = identity;
            CourseService = courseService;
        }

        public async Task<CourseImageEntity> GetPictureById(int id)
        {
            return await VerifyAndGetEntity(id);
        }

        public async Task<CourseImageEntity> GetPictureOfCourseById(int id)
        {
            var course = await CourseService.GetCourseByIdAsync(id);
            return await VerifyAndGetEntity(course.PictureId);
        }

        public async Task UploadFile(int id, IFormFile file, CancellationToken cancellationToken)
        {
            if (!ImageConstants.ContentTypes.Contains(file.ContentType))
                throw new ArgumentException("Content type is not accepted");

            var course = await CourseService.GetCourseByIdAsync(id);

            if (course.User?.Id != Identity.Id)
                throw new CourseImageBusinessException(CourseImageBusinessExceptionTypes.COURSE_UNAUTHORIZE, "Not authorize to upload image for this course");

            var image = await Repository.GetFirstByCriteria(entity => entity.CourseId == id);

            if (image == null)
            {
                image = new CourseImageEntity()
                {
                    CourseId = id
                };
            }

            image.Extention = file.ContentType;
            image.Data = await ProcessImage(file);

            /* using (var ms = new MemoryStream())
             {
                 file.CopyTo(ms);
                 image.Data = ms.ToArray();
             }*/

            await Repository.UpsertAsync(image);

            await CourseService.AddPictureId(image.CourseId, image.Id);
        }

        private async Task<byte[]> ProcessImage(IFormFile file)
        {
            int widthResult = 300;
            using var image = await Image.LoadAsync(file.OpenReadStream());

            var width = image.Width;
            var heigth = image.Height;

            if (width > widthResult)
            {
                heigth = widthResult / width * heigth;
                width = widthResult;
            }

            image.Mutate(i => i.Resize(new Size(width, heigth)));

            image.Metadata.ExifProfile = null;

            using var stream = new MemoryStream();

            Func<Task>? action = file.ContentType switch
            {
                "image/gif" => () => image.SaveAsGifAsync(stream),
                "image/jpeg" => () => image.SaveAsJpegAsync(stream),
                "image/png" => () => image.SaveAsPngAsync(stream),
                _ => null
            };

            if (action != null)
                await action();

            return stream.ToArray();
        }
    }
}