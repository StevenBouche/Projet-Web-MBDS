using Assignments.Business.Dto.Authentification;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assignments.API.Handlers.Authentification
{
    public class ActiveUserFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var identity = context.HttpContext.RequestServices.GetRequiredService<UserIdentity>();

            var i = new UserIdentity(context.HttpContext.User);

            identity.Id = i.Id;
            identity.Role = i.Role;
            identity.PictureId = i.PictureId;
            identity.Name = i.Name;

            return next();
        }
    }
}