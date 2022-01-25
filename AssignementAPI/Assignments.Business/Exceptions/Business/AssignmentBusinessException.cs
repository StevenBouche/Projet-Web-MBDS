using Assignments.Business.Extentions;
using System.ComponentModel;
using System.Net;

namespace Assignments.Business.Exceptions.Business
{
    public enum AssignmentBusinessExceptionTypes
    {
        [Description("Update")]
        ASSIGNMENT_UPDATE,
        [Description("Unauthorize")]
        ASSIGNMENT_UNAUTHORIZE
    }

    public class AssignmentBusinessException : BusinessException
    {
        public AssignmentBusinessExceptionTypes AssignmentType;
        public override HttpStatusCode HttpStatusCode => SelectHttpCode(AssignmentType);

        public AssignmentBusinessException(AssignmentBusinessExceptionTypes type, string message = "") : base(BusinessExceptionTypes.ASSIGNMENT, $"{type.ToDescriptionString()} {message}")
        {
            AssignmentType = type;
        }

        private HttpStatusCode SelectHttpCode(AssignmentBusinessExceptionTypes type)
        {
            return type switch
            {
                AssignmentBusinessExceptionTypes.ASSIGNMENT_UNAUTHORIZE => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.BadRequest
            };
        }
    }
}
