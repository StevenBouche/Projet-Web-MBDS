using Assignment.DAL.Models;
using AssignmentAPI.Models.Authentification.Security;
using AssignmentAPI.Models.User;

namespace AssignmentAPI.Services.User
{
    public interface IUserService : IBaseService<UserEntity>
    {
        Task<UserView?> CreateUserAsync(RegisterView element);
        Task<bool> DeleteUserByIdAsync(int id);
        IEnumerable<UserEntity> GetAllUser();
        UserEntity GetUserById(int id);
        UserEntity UpdateUser(UserEntity element);
        UserEntity? GetUserWithUserName(string name);
        void UpdateUserFromView(UserView element);
        IEnumerable<UserView> GetAllUserView();
        UserEntity GetUserWithRefreshToken(RefreshToken token);
    }
}
