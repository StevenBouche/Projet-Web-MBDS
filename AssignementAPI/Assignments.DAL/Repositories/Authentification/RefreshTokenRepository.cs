using Assignments.DAL.Context;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Assignments.DAL.Repositories.Authentification
{
    public class RefreshTokenRepository : BaseRepository<RefreshTokenEntity>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AssignmentContext context, ILogger<RefreshTokenRepository> logger) : base(context, logger)
        {
        }

        public Task<RefreshTokenEntity?> GetByTokenNameAsync(string token)
        {
            return DbSet.Where(refresh => refresh.Token == token).FirstOrDefaultAsync();
        }

        public Task<RefreshTokenEntity?> GetByUserIdAsync(int userId)
        {
            return DbSet.Where(refresh => refresh.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task DeleteByUserIdAsync(int userId)
        {
            var token = await GetByUserIdAsync(userId);
            if (token != null)
            {
                await DeleteAsync(token);
            }
        }
    }
}
