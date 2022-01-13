using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Assignments.API.Handlers.Authorization
{
    public class ShouldBeRequirement : IAuthorizationRequirement
    {
        public string Role { get; set; } = string.Empty;
    }

    public class RequirementHandler : AuthorizationHandler<ShouldBeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeRequirement requirement)
        {
            // check if Role claim exists - Else Return
            // (sort of Claim-based requirement)
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role))
                return Task.CompletedTask;

            // claim exists - retrieve the value
            var claim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            // check if the claim equals to either Admin or Editor
            // if satisfied, set the requirement as success
            if (claim != null && claim.Value == requirement.Role)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
