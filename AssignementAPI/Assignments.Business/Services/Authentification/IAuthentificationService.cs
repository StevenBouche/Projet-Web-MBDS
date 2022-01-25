using Assignments.Business.Dto.Authentification;
using Assignments.Business.Dto.Authentification.Tokens;
using Assignments.Business.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.Business.Services.Authentification
{
    public interface IAuthentificationService : IBaseService<RefreshTokenEntity>
    {
        Task<LoginResult> LoginAsync(LoginForm login);

        Task<LoginResult> RefreshLoginAsync(RefreshToken token);

        Task LogoutAsync(UserIdentity? userAccount);
    }
}