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
            var haveWork = identity != null ? identity.Role switch
            {
                "STUDENT" => entity.WorkSubmits.Any(e => e.UserId == identity.Id),
                "PROFESSOR" => !(entity.Course != null && entity.Course.UserId == identity.Id && entity.WorkSubmits.Any(e => e.State == DAL.Enumerations.WorkSubmitState.SUBMITTED)),
                _ => false
            } : false; 

            return new Assignment()
            {
                Id = entity.Id,
                State = entity.State,
                DelivryDate = entity.DelivryDate,
                DeliveryDateLabel = entity.DelivryDate.ToString("F", CultureInfo.CreateSpecificCulture("en-US")),
                Label = entity.Label,
                Course = entity.Course?.ToCourse(),
                HaveWork = haveWork
            };
        }
    }
}