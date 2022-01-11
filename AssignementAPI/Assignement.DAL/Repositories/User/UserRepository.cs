using Assignment.DAL.Context;
using Assignment.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Assignment.DAL.Repositories.User
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(AssignmentContext context, ILogger<UserRepository> logger) : base(context, logger)
        {

        }

        public UserEntity? GetByName(string name)
        {
            return this.DbSet.FirstOrDefault(user => user.Name == name);
        }
    }
}
