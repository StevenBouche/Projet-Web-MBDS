using Assignments.API.Extentions;
using System.ComponentModel;

namespace Assignments.API.Exceptions.Authorization
{
    public enum AuthorizationExceptionTypes
    {
        [Description("Unauthorized")]
        UNAUTHORIZED,
        [Description("No claims found")]
        NO_CLAIMS
    }

    public class AuthorizationException : Exception
    {
        public AuthorizationExceptionTypes Type;

        public AuthorizationException(AuthorizationExceptionTypes type) : base(type.ToDescriptionString())
        {
            Type = type;
        }
    }
}
