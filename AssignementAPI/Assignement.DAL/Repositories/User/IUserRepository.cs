using Assignment.DAL.Models;

namespace Assignment.DAL.Repositories.User
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        public UserEntity? GetByName(string name);
    }
}
