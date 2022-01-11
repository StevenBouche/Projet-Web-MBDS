using Assignment.DAL.Context;
using Assignment.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Assignment.DAL.Repositories.Course
{
    public class CourseRepository : BaseRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(AssignmentContext context, ILogger<CourseRepository> logger) : base(context, logger)
        {

        }
    }
}
