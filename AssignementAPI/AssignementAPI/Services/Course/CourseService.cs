using Assignment.DAL.Models;
using Assignment.DAL.Repositories.Course;

namespace AssignmentAPI.Services.Course
{
    public class CourseService : BaseService<CourseEntity, ICourseRepository>, ICourseService
    {
        public CourseService(ICourseRepository repository, ILogger<CourseService> logger) : base(repository, logger)
        {
        }
    }
}
