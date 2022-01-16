using Assignments.API.Configurations.Authorization;
using Assignments.API.Exceptions.Authorization;
using Assignments.API.Exceptions.Business;
using Assignments.API.Exceptions.Entities;
using Assignments.API.Models.Api;
using Assignments.API.Models.Authentification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Assignments.API.Controllers.Base
{
    public class BaseAssignmentController : ControllerBase
    {

        protected readonly ILogger<BaseAssignmentController> Logger;
        protected readonly UserIdentity Identity;

        public BaseAssignmentController(UserIdentity identity, ILogger<BaseAssignmentController> logger)
        {
            Logger = logger;
            Identity = identity;
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
                case EntityException e:
                    return LogInfoAndReturn(exception, e.HttpStatusCode);
                case BusinessException e:
                    return LogInfoAndReturn(exception, e.HttpStatusCode);
                case AuthorizationException _:
                    return LogInfoAndReturn(exception, HttpStatusCode.Forbidden);
                case ArgumentException _:
                    return LogErrorAndReturn(exception, HttpStatusCode.BadRequest);
                default:
                    return LogErrorAndReturn(exception, HttpStatusCode.InternalServerError);
            }
        }

        protected ActionResult LogErrorAndReturn(Exception error, HttpStatusCode code)
        {
            Logger.LogError(error, GetErrorString(error));
            return StatusCode((int)code, GetReturnResponse(error, code));
        }

        protected ActionResult LogWarningAndReturn(Exception error, HttpStatusCode code)
        {
            Logger.LogWarning(error, GetErrorString(error));
            return StatusCode((int)code, GetReturnResponse(error, code));
        }

        protected ActionResult LogInfoAndReturn(Exception error, HttpStatusCode code)
        {
            Logger.LogInformation(error, GetErrorString(error));
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
