using Assignments.API.Extentions;
using System.Net;

namespace Assignments.API.Exceptions.Business
{
    public enum CourseBusinessExceptionTypes
    {

        COURSE_UNAUTHORIZE
    }

    public class CourseBusinessException : BusinessException
    {
        public CourseBusinessExceptionTypes AssignmentType;
        public override HttpStatusCode HttpStatusCode => SelectHttpCode(AssignmentType);

        public CourseBusinessException(CourseBusinessExceptionTypes type, string message = "") : base(BusinessExceptionTypes.COURSE, $"{type.ToDescriptionString()} {message}")
        {
            AssignmentType = type;
        }

        private HttpStatusCode SelectHttpCode(CourseBusinessExceptionTypes type)
        {
            return type switch
            {
                _ => HttpStatusCode.BadRequest
            };
        }
    }
}
