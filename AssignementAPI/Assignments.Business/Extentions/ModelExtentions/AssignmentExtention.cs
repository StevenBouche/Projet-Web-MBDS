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
    }
}