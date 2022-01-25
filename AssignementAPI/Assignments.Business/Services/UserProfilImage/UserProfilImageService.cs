using Assignments.Business.Dto.Authentification;
using Assignments.Business.Dto.Image;
using Assignments.Business.Services.Base;
using Assignments.Business.Services.Users;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.UserProfilImage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Assignments.Business.Services.UserProfilImage
{
    public class UserProfilImageService : BaseService<UserProfilImageEntity, IUserProfilImageRepository>, IUserProfilImageService
    {
        private readonly UserIdentity Identity;
        private readonly IUserService UserService;

        public UserProfilImageService(IUserProfilImageRepository repository, IUserService userService, UserIdentity identity, ILogger<UserProfilImageService> logger) : base(repository, logger)
        {
            Identity = identity;
            UserService = userService;
        }

        public async Task<UserProfilImageEntity> GetPictureById(int? id)
        {
            return await VerifyAndGetEntity(id);
        }

        public async Task<UserProfilImageEntity> GetPictureByUserId(int id)
        {
            var user = await UserService.GetUserByIdAsync(id);
            return await GetPictureById(user.Image?.Id ?? null);
        }

        public async Task UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            if (!ImageConstants.ContentTypes.Contains(file.ContentType))
                throw new ArgumentException("Content type is not accepted");

            var image = await Repository.GetFirstByCriteria(entity => entity.UserId == Identity.Id);

            if (image == null)
            {
                image = new UserProfilImageEntity()
                {
                    UserId = Identity.Id
                };
            }

            image.Extention = file.ContentType;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                image.Data = ms.ToArray();
            }

            await Repository.UpsertAsync(image);

            await UserService.AddPictureId(image.UserId, image.Id);
        }
    }
}