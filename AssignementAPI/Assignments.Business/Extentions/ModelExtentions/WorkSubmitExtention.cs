using Assignments.Business.Dto.WorkSubmits;
using Assignments.DAL.Models;

namespace Assignments.Business.Extentions.ModelExtentions
{
    public static class WorkSubmitExtention
    {
        public static WorkSubmit ToWorkSubmit(this WorkSubmitEntity entity)
        {
            var ass = entity.Assignment?.ToAssignment();
            var submittedDate = entity.SubmittedDate;

            var isLate = ass != null && DateTime.Compare(submittedDate ?? DateTime.Now, ass.DelivryDate) > 0;

            return new WorkSubmit()
            {
                Id = entity.Id,
                Label = entity.Label,
                Grade = entity.Grade,
                Comment = entity.Comment,
                Description = entity.Description,
                State = entity.State,
                SubmittedDate = submittedDate,
                Assignment = ass,
                User = entity.User?.ToUser(),
                IsLate = isLate
            };
        }
    }
}