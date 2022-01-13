using Assignments.API.Configurations.Authorization;
using Assignments.API.Models.Authentification;
using Assignments.DAL.Enumerations;
using Microsoft.Extensions.Options;

namespace Assignments.API.Services.Authorization
{
    public class AuthorizationService
    {

        private readonly AuthorizationConfig Configuration;

        public AuthorizationService(IOptions<AuthorizationConfig> configuratioon)
        {
            Configuration = configuratioon.Value;
        }

        public bool IsAuthorize(UserIdentity? identity, AuthorizationTypes type)
        {
            if (identity == null) return false;

            Configuration.Authorizations.TryGetValue(type, out List<UserRoles>? roles);

            if (roles != null && Enum.TryParse(identity.Role, out UserRoles roleenum))
            {
                return roles.Contains(roleenum);
            }

            return false;
        }

    }
}
