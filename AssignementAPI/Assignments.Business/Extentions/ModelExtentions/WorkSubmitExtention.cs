using Assignments.Business.Dto.WorkSubmits;
using Assignments.DAL.Models;

namespace Assignments.Business.Extentions.ModelExtentions
{
    public static class WorkSubmitExtention
    {
        public static WorkSubmit ToWorkSubmit(this WorkSubmitEntity entity)
        {
            return new WorkSubmit()
            {
                Id = entity.Id,
                Label = entity.Label,
                Grade = entity.Grade,
                Comment = entity.Comment,
                Description = entity.Description,
                State = entity.State,
                Assignment = entity.Assignment?.ToAssignment(),
                User = entity.User?.ToUser()
            };
        }
    }
}