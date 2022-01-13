using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;

namespace Assignments.DAL.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        public UserEntity? GetByName(string name);
    }
}
