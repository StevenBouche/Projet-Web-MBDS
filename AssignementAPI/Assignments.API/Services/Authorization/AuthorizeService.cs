using Assignments.API.Configurations.Authorization;
using Assignments.API.Exceptions.Authorization;
using Assignments.API.Models.Authentification;
using Assignments.DAL.Enumerations;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Assignments.API.Services.Authorization
{
    public class AuthorizeService : IAuthorizeService
    {

        private readonly AuthorizationConfig Configuration;

        public AuthorizeService(IOptions<AuthorizationConfig> configuratioon)
        {
            Configuration = configuratioon.Value;
        }

        public UserIdentity HaveClaims(ClaimsPrincipal? claims)
        {
            if (claims == null)
                throw new AuthorizationException(AuthorizationExceptionTypes.NO_CLAIMS);
            else
                return new UserIdentity(claims);
        }

        public void IsAuthorize(UserIdentity identity, AuthorizationTypes type)
        {
            var isAuthorize = true;

            if (identity == null) 
                isAuthorize = false;

            if(isAuthorize && 
                Configuration.Authorizations.TryGetValue(type, out List<UserRoles>? roles) && 
                Enum.TryParse(identity.Role, out UserRoles roleenum))
            {
                isAuthorize = roles.Contains(roleenum);
            }

            if (!isAuthorize)
                throw new AuthorizationException(AuthorizationExceptionTypes.UNAUTHORIZED);
        }

    }
}
