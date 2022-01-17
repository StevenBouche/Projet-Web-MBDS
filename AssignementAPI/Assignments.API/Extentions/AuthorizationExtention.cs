using Assignments.API.Configurations.Authorization;
using Assignments.API.Handlers.Authorization;
using Assignments.API.Models.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

namespace Assignments.API.Extentions
{
    public static class AuthorizationExtention
    {
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            //services.Configure<AuthorizationConfig>((_) => new AuthorizationConfig());
           // services.AddSingleton<IAuthorizationHandler, RequirementHandler>();

           /* services.AddAuthorization(config =>
            {
                config.AddPolicy(AuthorizationConstants.AuthorizationPolicy_Admin, options =>
                {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.RequireClaim(ClaimTypes.Role);
                    //tions.Requirements.Add(new ShouldBeRequirement() { Role = "ADMIN" });
                });
                config.AddPolicy(AuthorizationConstants.AuthorizationPolicy_Student, options =>
                {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.RequireClaim(ClaimTypes.Role);
                    //tions.Requirements.Add(new ShouldBeRequirement() { Role = "STUDENT" });
                });
                config.AddPolicy(AuthorizationConstants.AuthorizationPolicy_Professor, options =>
                {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.RequireClaim(ClaimTypes.Role);
                    //tions.Requirements.Add(new ShouldBeRequirement() { Role = "PROFESSOR" });
                });
            });*/
        }
    }
}
