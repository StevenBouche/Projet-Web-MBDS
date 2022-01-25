using Assignments.Business.Extentions;
using System.ComponentModel;
using System.Net;

namespace Assignments.Business.Exceptions.Business
{
    public enum BusinessExceptionTypes
    {
        [Description("Course business")]
        COURSE,
        [Description("WorkSubmit business")]
        WORK_SUBMIT,
        [Description("Assignment business")]
        ASSIGNMENT,
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
