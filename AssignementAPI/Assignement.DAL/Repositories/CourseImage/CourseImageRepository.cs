using Assignment.DAL.Context;
using Assignment.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Assignment.DAL.Repositories.CourseImage
{
    public class CourseImageRepository : BaseRepository<CourseImageEntity>, ICourseImageRepository
    {
        public CourseImageRepository(AssignmentContext context, ILogger<CourseImageRepository> logger) : base(context, logger)
        {

        }
    }
}
