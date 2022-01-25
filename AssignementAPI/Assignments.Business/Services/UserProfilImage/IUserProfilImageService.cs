using Assignments.Business.Services.Base;
using Assignments.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace Assignments.Business.Services.UserProfilImage
{
    public interface IUserProfilImageService : IBaseService<UserProfilImageEntity>
    {
        Task UploadFile(IFormFile file, CancellationToken cancellationToken);

        Task<UserProfilImageEntity> GetPictureById(int? id);

        Task<UserProfilImageEntity> GetPictureByUserId(int id);
    }
}