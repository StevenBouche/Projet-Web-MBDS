using Assignments.API.Models.Authentification.Tokens;
using Assignments.API.Models.Users;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Users;

namespace Assignments.API.Services.Users
{
    public class UserService : BaseService<UserEntity, IUserRepository>, IUserService
    {
        public UserService(IUserRepository repository, ILogger<UserService> logger) : base(repository, logger)
        {
        }

        public async Task<User?> CreateUserAsync(UserForm element)
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

                return new User()
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

        public IEnumerable<User> GetAllUserView()
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

        public void UpdateUserFromView(User element)
        {
            throw new NotImplementedException();
        }
    }
}
