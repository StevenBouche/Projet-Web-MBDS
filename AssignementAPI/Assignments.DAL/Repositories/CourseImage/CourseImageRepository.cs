using Assignments.DAL.Context;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Assignments.DAL.Repositories.CourseImage
{
    public class CourseImageRepository : BaseRepository<CourseImageEntity>, ICourseImageRepository
    {
        public CourseImageRepository(AssignmentContext context, ILogger<CourseImageRepository> logger) : base(context, logger)
        {

        }
    }
}
