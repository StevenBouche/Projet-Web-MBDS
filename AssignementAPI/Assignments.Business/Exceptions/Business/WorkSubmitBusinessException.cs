using Assignments.Business.Extentions;
using System.ComponentModel;
using System.Net;

namespace Assignments.Business.Exceptions.Business
{
    public enum WorkSubmitBusinessExceptionTypes
    {
        [Description("Unauthorize evaluate")]
        WORK_EVALUATE_UNAUTHORIZE,
        [Description("Update evaluation")]
        WORK_EVALUATE_UPDATE,
        [Description("Update work")]
        WORK_SUBMIT_UPDATE,
        [Description("Unauthorize work")]
        WORK_SUBMIT_UNAUTHORIZE,
    }

    public class WorkSubmitBusinessException : BusinessException
    {
        public WorkSubmitBusinessExceptionTypes WorkSubmitType;
        public override HttpStatusCode HttpStatusCode => SelectHttpCode(WorkSubmitType);

        public WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes type, string message = "") : base(BusinessExceptionTypes.WORK_SUBMIT, $"{type.ToDescriptionString()} {message}")
        {
            WorkSubmitType = type;
        }

        private HttpStatusCode SelectHttpCode(WorkSubmitBusinessExceptionTypes type)
        {
            return type switch
            {
                WorkSubmitBusinessExceptionTypes.WORK_SUBMIT_UNAUTHORIZE => HttpStatusCode.Forbidden,
                WorkSubmitBusinessExceptionTypes.WORK_EVALUATE_UNAUTHORIZE => HttpStatusCode.Forbidden,
                _ => HttpStatusCode.BadRequest
            };
        }
    }
}
