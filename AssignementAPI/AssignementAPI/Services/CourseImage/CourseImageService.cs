using Assignment.DAL.Models;
using Assignment.DAL.Repositories.CourseImage;

namespace AssignmentAPI.Services.CourseImage
{
    public class CourseImageService : BaseService<CourseImageEntity, ICourseImageRepository>, ICourseImageService
    {
        public CourseImageService(ICourseImageRepository repository, ILogger<CourseImageService> logger) : base(repository, logger)
        {
        }
    }
}
