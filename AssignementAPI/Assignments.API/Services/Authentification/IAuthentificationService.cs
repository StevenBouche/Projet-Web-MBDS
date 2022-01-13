using Assignments.API.Models.Authentification;
using Assignments.API.Models.Authentification.Tokens;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.API.Services.Authentification
{
    public interface IAuthentificationService : IBaseService<RefreshTokenEntity>
    {
        Task<LoginResult> LoginAsync(LoginForm login);
        Task<LoginResult> RefreshLoginAsync(RefreshToken token);
        Task LogoutAsync(UserIdentity? userAccount);
    }
}