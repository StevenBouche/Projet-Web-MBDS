using Assignments.DAL.Context;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Assignments.DAL.Repositories.Courses
{
    public class CourseRepository : BaseRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(AssignmentContext context, ILogger<CourseRepository> logger) : base(context, logger)
        {
        }

        public IEnumerable<CourseEntity> GetStudentCoursesAsync(int userId)
        {
            return Context.WorkSubmits
                .Where(work => work.UserId == userId)
                .Select(work => work.Assignment != null ? work.Assignment.CourseId : 0)
                .Where(id => id > 0)
                .Distinct()
                .Join(Context.Courses, courseID => courseID, course => course.Id, (courseId, course) => course)
                .AsEnumerable();
        }
    }
}