using Assignment.DAL.Models;
using Assignment.DAL.Repositories.User;
using AssignmentAPI.Models.Authentification.Security;
using AssignmentAPI.Models.User;

namespace AssignmentAPI.Services.User
{
    public class UserService : BaseService<UserEntity, IUserRepository>, IUserService
    {
        public UserService(IUserRepository repository, ILogger<UserService> logger) : base(repository, logger)
        {
        }

        public async Task<UserView?> CreateUserAsync(RegisterView element)
        {
            int nbAccountWithSameMail = await Repository.CountAsync(acc => acc.Name.Equals(element.Name));

            if (nbAccountWithSameMail == 0)
            {
                var entity = new UserEntity()
                {
                    Name = element.Name,
                    Password = element.Password
                };

                await Repository.AddAsync(entity);

                return new UserView()
                {
                    Id = entity.Id,
                    Name = element.Name,
                    Role = entity.Role.ToString(),
                    PictureId = entity.ImageId
                };
            }

            return null;
        }

        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            return await Repository.DeleteByIdAsync(id);
        }

        public IEnumerable<UserEntity> GetAllUser()
        {
            return Repository.Set.AsEnumerable();
        }

        public IEnumerable<UserView> GetAllUserView()
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserWithRefreshToken(RefreshToken token)
        {
            throw new NotImplementedException();
        }

        public UserEntity? GetUserWithUserName(string name)
        {
            return Repository.GetByName(name);
        }

        public UserEntity UpdateUser(UserEntity element)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserFromView(UserView element)
        {
            throw new NotImplementedException();
        }
    }
}
