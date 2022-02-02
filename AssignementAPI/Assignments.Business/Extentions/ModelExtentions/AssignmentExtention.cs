using Assignments.Business.Dto.Assignments;
using Assignments.Business.Dto.Authentification;
using Assignments.DAL.Models;
using System.Globalization;

namespace Assignments.Business.Extentions.ModelExtentions
{
    public static class AssignmentExtention
    {
        public static Assignment ToAssignment(this AssignmentEntity entity, UserIdentity? identity = null)
        {
            return new Assignment()
            {
                Id = entity.Id,
                State = entity.State,
                DelivryDate = entity.DelivryDate,
                DeliveryDateLabel = entity.DelivryDate.ToString("F", CultureInfo.CreateSpecificCulture("en-US")),
                Label = entity.Label,
                Course = entity.Course?.ToCourse(),
                HaveWork = identity != null && identity.Role == "STUDENT" ? entity.WorkSubmits.Any(e => e.UserId == identity.Id) : false
            };
        }
    }
}