using Assignments.Business.Dto.Authentification;
using Assignments.Business.Services.Users;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assignments.API.Handlers.Authentification
{
    public class ActiveUserFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var identity = context.HttpContext.RequestServices.GetRequiredService<UserIdentity>();
            var service = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

            var i = new UserIdentity(context.HttpContext.User);


            var user = service.GetUserById(i.Id);

            if(user != null)
            {
                identity.Id = i.Id;
                identity.Role = user.Role.ToString();
                identity.PictureId = user.Image?.Id;
                identity.Name = user.Name;
                identity.UpdatedAt = user.UpdatedDate;
            }
 
            return next();
        }
    }
}