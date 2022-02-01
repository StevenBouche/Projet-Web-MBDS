using Assignments.Business.Extentions;
using System.ComponentModel;
using System.Net;

namespace Assignments.Business.Exceptions.Business
{
    public enum UserBusinessExceptionTypes
    {
        [Description("User already exist")]
        USER_ALREADY_EXIST,
        [Description("Not authorize action")]
        USER_UNAUTHORIZE
    }

    public class UserBusinessException : BusinessException
    {
        public UserBusinessExceptionTypes AssignmentType;
        public override HttpStatusCode HttpStatusCode => SelectHttpCode(AssignmentType);

        public UserBusinessException(UserBusinessExceptionTypes type, string message = "") : base(BusinessExceptionTypes.USER, $"{type.ToDescriptionString()} {message}")
        {
            AssignmentType = type;
        }

        private HttpStatusCode SelectHttpCode(UserBusinessExceptionTypes type)
        {
            return type switch
            {
                _ => HttpStatusCode.BadRequest
            };
        }
    }
}
