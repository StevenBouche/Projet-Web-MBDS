using Assignments.DAL.Context;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Assignments.DAL.Repositories.Users
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(AssignmentContext context, ILogger<UserRepository> logger) : base(context, logger)
        {

        }

        public UserEntity? GetByName(string name)
        {
            return DbSet.FirstOrDefault(user => user.Name == name);
        }
    }
}
