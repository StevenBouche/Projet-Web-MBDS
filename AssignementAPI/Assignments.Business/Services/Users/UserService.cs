using Assignments.Business.Dto.Authentification.Tokens;
using Assignments.Business.Dto.Authorization;
using Assignments.Business.Dto.Users;
using Assignments.Business.Services.Base;
using Assignments.DAL.Enumerations;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Users;
using Microsoft.Extensions.Logging;

namespace Assignments.Business.Services.Users
{
    public class UserService : BaseService<UserEntity, IUserRepository>, IUserService
    {
        public UserService(IUserRepository repository, ILogger<UserService> logger) : base(repository, logger)
        {
        }

        public async Task AddPictureId(int id, int id1)
        {
            var entity = await VerifyAndGetEntity(id);

            //entity.ImageId = id1; // todo remove

            await Repository.UpdateAsync(entity);
        }

        public Task AddRefreshTokenId(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> CreateUserAsync(UserForm element)
        {
            var withSameMail = await Repository.AnyByCriteria(acc => acc.Name.Equals(element.Name));

            if (!withSameMail)
            {
                var entity = new UserEntity()
                {
                    Name = element.Name,
                    Password = element.Password
                };

                entity.Role = element.Role switch
                {
                    AuthorizationConstants.STUDENT => UserRoles.STUDENT,
                    AuthorizationConstants.PROFESSOR => UserRoles.PROFESSOR,
                    AuthorizationConstants.ADMIN => UserRoles.ADMIN,
                    _ => UserRoles.NONE
                };

                await Repository.AddAsync(entity);

                return new User()
                {
                    Id = entity.Id,
                    Name = element.Name,
                    Role = entity.Role.ToString(),
                    PictureId = entity.Image?.Id ?? null
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

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            return await VerifyAndGetEntity(id);
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