using Assignments.Business.Dto.Courses;
using Assignments.DAL.Models;

namespace Assignments.Business.Extentions.ModelExtentions
{
    public static class CourseExtention
    {
        public static Course ToCourse(this CourseEntity entity)
        {
            return new Course()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                PictureId = entity.ImageId,
                User = entity.User?.ToUser(),
                Stats = new CourseStats()
                {
                    TotalAssignments = entity.Assignments.Count,
                    TotalWorks = entity.Assignments.Select(assignment => assignment.WorkSubmits.Count).Sum()
                },
                CreateAt = entity.CreatedDate,
                UpdateAt = entity.UpdatedDate
            };
        }
    }
}