using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;

namespace Assignments.DAL.Repositories.Authentification
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshTokenEntity>
    {
        Task<RefreshTokenEntity?> GetByUserIdAsync(int userId);
        Task<RefreshTokenEntity?> GetByTokenNameAsync(string token);
        Task DeleteByUserIdAsync(int userId);
    }
}