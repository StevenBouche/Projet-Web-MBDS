using Assignments.API.Exceptions.Authorization;
using Assignments.API.Models.Api;
using Assignments.API.Models.Authentification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Assignments.API.Controllers.Base
{
    public class BaseAssignmentController : ControllerBase
    {

        protected readonly ILogger<BaseAssignmentController> logger;

        protected UserIdentity? Identity
        {
            get
            {
                return User != null ? new UserIdentity(User) : null;
            }
        }

        protected BaseAssignmentController(ILogger<BaseAssignmentController> logger)
        {
            this.logger = logger;
        }

        protected async Task<ActionResult> TryExecuteAsync<T>(Func<Task<T>> action)
            where T : ActionResult
        {
            try
            {
                return await action();
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        protected ActionResult TryExecute<T>(Func<T> action)
            where T : ActionResult
        {
            try
            {
                return action();
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        protected virtual ActionResult HandleException(Exception exception)
        {
            switch (exception)
            {
                case AuthorizationException _:
                    return LogErrorAndReturn(exception, HttpStatusCode.Unauthorized);
                case ArgumentException _:
                default:
                    logger?.LogError(exception, GetErrorString(exception));
                    return StatusCode((int)HttpStatusCode.InternalServerError, GetReturnResponse(HttpStatusCode.InternalServerError));
            }
        }

        protected ActionResult LogErrorAndReturn(Exception error, HttpStatusCode code)
        {
            logger.LogError(error, GetErrorString(error));
            return StatusCode((int)code, GetReturnResponse(error, code));
        }

        protected ActionResult LogWarningAndReturn(Exception error, HttpStatusCode code)
        {
            logger.LogWarning(error, GetErrorString(error));
            return StatusCode((int)code, GetReturnResponse(error, code));
        }

        protected ActionResult LogInfoAndReturn(Exception error, HttpStatusCode code)
        {
            logger.LogInformation(error, GetErrorString(error));
            return StatusCode((int)code, GetReturnResponse(error, code));
        }

        private string GetErrorString(Exception error)
        {
            return $"{error.GetType().Name}:{error.Message}";
        }

        private ApiErrorResponse GetReturnResponse(Exception e, HttpStatusCode code)
        {
            return new ApiErrorResponse()
            {
                StatusCode = (int)code,
                Message = e.Message
            };
        }

        private ApiErrorResponse GetReturnResponse(HttpStatusCode code)
        {
            return new ApiErrorResponse()
            {
                StatusCode = (int)code,
                Message = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError)
            };
        }
    }
}
