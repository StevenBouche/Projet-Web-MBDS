using Assignments.Business.Dto.Authentification.Tokens;
using Assignments.Business.Dto.Users;
using Assignments.Business.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.Business.Services.Users
{
    public interface IUserService : IBaseService<UserEntity>
    {
        Task<User?> CreateUserAsync(UserForm element);

        Task<bool> DeleteUserByIdAsync(int id);

        IEnumerable<UserEntity> GetAllUser();

        Task<UserEntity> GetUserByIdAsync(int id);

        UserEntity? GetUserById(int id);

        UserEntity UpdateUser(UserEntity element);

        UserEntity? GetUserWithUserName(string name);

        void UpdateUserFromView(User element);

        IEnumerable<User> GetAllUserView();

        UserEntity GetUserWithRefreshToken(RefreshToken token);

        Task AddPictureId(int id, int id1);

        Task AddRefreshTokenId(int id1, int id2);
    }
}