using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AssignmentAPI.Configurations.Authorization
{
    public static class AuthorizationExtention
    {
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, RequirementHandler>();

            services.AddAuthorization(config => {
                config.AddPolicy("Admin", options => {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.RequireClaim(ClaimTypes.Role);
                    options.Requirements.Add(new ShouldBeRequirement() { Role = "ADMIN" });
                });
                config.AddPolicy("Student", options => {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.RequireClaim(ClaimTypes.Role);
                    options.Requirements.Add(new ShouldBeRequirement() { Role = "STUDENT" });
                });
                config.AddPolicy("Professor", options => {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.RequireClaim(ClaimTypes.Role);
                    options.Requirements.Add(new ShouldBeRequirement() { Role = "PROFESSOR" });
                });
            });
        }
    }
}
