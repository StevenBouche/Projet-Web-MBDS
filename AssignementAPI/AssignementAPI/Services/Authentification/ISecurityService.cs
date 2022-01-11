using Assignment.DAL.Models;
using AssignmentAPI.Models.Authentification;
using AssignmentAPI.Models.Authentification.Security;
using AssignmentAPI.Models.Authentification.View;

namespace AssignmentAPI.Services.Authentification
{
    public interface ISecurityService : IBaseService<RefreshTokenEntity>
    {
        Task<LoginResult> LoginAsync(LoginView login);
        Task<LoginResult> RefreshLoginAsync(RefreshToken token);
        Task LogoutAsync(UserIdentity? userAccount);
    }
}