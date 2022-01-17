using Assignments.API.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.API.Services.UserProfilImage
{
    public interface IUserProfilImageService : IBaseService<UserProfilImageEntity>
    {
        Task UploadFile(IFormFile file, CancellationToken cancellationToken);
        Task<UserProfilImageEntity> GetPictureById(int? id);
        Task<UserProfilImageEntity> GetPictureByUserId(int id);
    }
}
