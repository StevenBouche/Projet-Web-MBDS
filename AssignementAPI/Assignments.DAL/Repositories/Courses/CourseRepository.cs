using Assignments.DAL.Context;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Assignments.DAL.Repositories.Courses
{
    public class CourseRepository : BaseRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(AssignmentContext context, ILogger<CourseRepository> logger) : base(context, logger)
        {

        }
    }
}
