using Assignments.API.Extentions;
using System.ComponentModel;
using System.Net;

namespace Assignments.API.Exceptions.Business
{
    public enum BusinessExceptionTypes
    {
        [Description("WorkSubmit business")]
        WORK_SUBMIT,
    }

    public abstract class BusinessException : Exception
    {
        public abstract HttpStatusCode HttpStatusCode { get; }
        public BusinessExceptionTypes Type;

        public BusinessException(BusinessExceptionTypes type, string message) : base($"{type.ToDescriptionString()} : {message}")
        {
            Type = type;
        }
    }
}
