using Assignments.API.Configurations.Authorization;
using Assignments.API.Models.Authentification;
using System.Security.Claims;

namespace Assignments.API.Services.Authorization
{
    public interface IAuthorizeService
    {
        UserIdentity HaveClaims(ClaimsPrincipal? claims);
        void IsAuthorize(UserIdentity identity, AuthorizationTypes type);
    }
}
