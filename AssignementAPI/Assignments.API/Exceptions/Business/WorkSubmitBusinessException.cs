using Assignments.API.Extentions;
using System.ComponentModel;
using System.Net;

namespace Assignments.API.Exceptions.Business
{
    public enum WorkSubmitBusinessExceptionTypes
    {
        [Description("Unauthorize to evaluate this work")]
        WORK_EVALUATE_UNAUTHORIZE,
        [Description("Cannot evaluate an unsubmitted work")]
        WORK_EVALUATE_IS_NOT_SUBMITTED,
        [Description("Cannot update an un work where is not in created state")]
        WORK_SUBMIT_IS_NOT_CREATED,
        [Description("Unauthorize to update/submit this work")]
        WORK_SUBMIT_UNAUTHORIZE,
        [Description("Cannot submit a work where doesnt have assignment link")]
        WORK_SUBMIT_IS_NOT_LINK_ASSIGNMENT
    }

    public class WorkSubmitBusinessException : BusinessException
    {
        public WorkSubmitBusinessExceptionTypes WorkSubmitType;
        public override HttpStatusCode HttpStatusCode => SelectHttpCode(WorkSubmitType);

        public WorkSubmitBusinessException(WorkSubmitBusinessExceptionTypes type) : base(BusinessExceptionTypes.WORK_SUBMIT, type.ToDescriptionString())
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
