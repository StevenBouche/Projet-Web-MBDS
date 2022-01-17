using Assignments.API.Models.Authentification.Tokens;
using Assignments.API.Models.Users;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.API.Services.Users
{
    public interface IUserService : IBaseService<UserEntity>
    {
        Task<User?> CreateUserAsync(UserForm element);
        Task<bool> DeleteUserByIdAsync(int id);
        IEnumerable<UserEntity> GetAllUser();
        Task<UserEntity> GetUserByIdAsync(int id);
        UserEntity UpdateUser(UserEntity element);
        UserEntity? GetUserWithUserName(string name);
        void UpdateUserFromView(User element);
        IEnumerable<User> GetAllUserView();
        UserEntity GetUserWithRefreshToken(RefreshToken token);
        Task AddPictureId(int id, int id1);
    }
}
