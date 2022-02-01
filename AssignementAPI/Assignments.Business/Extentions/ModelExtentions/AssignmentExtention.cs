using Assignments.Business.Dto.Assignments;
using Assignments.DAL.Models;

namespace Assignments.Business.Extentions.ModelExtentions
{
    public static class AssignmentExtention
    {
        public static Assignment ToAssignment(this AssignmentEntity entity)
        {
            return new Assignment()
            {
                Id = entity.Id,
                State = entity.State,
                DelivryDate = entity.DelivryDate,
                Label = entity.Label,
                CourseId = entity.CourseId
            };
        }

        public static AssignmentDetails ToAssignmentDetails(this AssignmentEntity entity)
        {
            return new AssignmentDetails()
            {
                Id = entity.Id,
                State = entity.State,
                DelivryDate = entity.DelivryDate,
                Label = entity.Label,
                CourseId = entity.CourseId,
                CourseName = entity.Course.Name,
                CourseDescription = entity.Course.Description,
                CoursePictureId = entity.Course.ImageId
            };
        }
    }
}