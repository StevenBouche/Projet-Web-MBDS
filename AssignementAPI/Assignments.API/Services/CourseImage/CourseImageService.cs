using Assignments.API.Services.Base;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.CourseImage;

namespace Assignments.API.Services.CourseImage
{
    public class CourseImageService : BaseService<CourseImageEntity, ICourseImageRepository>, ICourseImageService
    {
        public CourseImageService(ICourseImageRepository repository, ILogger<CourseImageService> logger) : base(repository, logger)
        {
        }
    }
}
