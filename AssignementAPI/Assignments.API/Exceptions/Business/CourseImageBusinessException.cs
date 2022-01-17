using Assignments.API.Extentions;
using System.Net;

namespace Assignments.API.Exceptions.Business
{
    public enum CourseImageBusinessExceptionTypes
    {
        COURSE_UNAUTHORIZE
    }

    public class CourseImageBusinessException : BusinessException
    {
        public CourseImageBusinessExceptionTypes AssignmentType;
        public override HttpStatusCode HttpStatusCode => SelectHttpCode(AssignmentType);

        public CourseImageBusinessException(CourseImageBusinessExceptionTypes type, string message = "") : base(BusinessExceptionTypes.COURSE, $"{type.ToDescriptionString()} {message}")
        {
            AssignmentType = type;
        }

        private HttpStatusCode SelectHttpCode(CourseImageBusinessExceptionTypes type)
        {
            return type switch
            {
                _ => HttpStatusCode.BadRequest
            };
        }
    }
}
