using Assignments.DAL.Enumerations;

namespace Assignments.API.Configurations.Authorization
{
    public enum AuthorizationTypes
    {
        CREATE_COURSE
    }

    public class AuthorizationConfig
    {

        public Dictionary<AuthorizationTypes, List<UserRoles>> Authorizations = new();

        public AuthorizationConfig()
        {
            InitializeAuthorizations();
        }

        private void InitializeAuthorizations()
        {
            Authorizations.Add(AuthorizationTypes.CREATE_COURSE, new List<UserRoles>() { UserRoles.PROFESSOR });
        }
    }
}
