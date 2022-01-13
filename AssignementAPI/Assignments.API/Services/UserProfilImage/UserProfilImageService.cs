using Assignments.API.Services.Base;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.UserProfilImage;

namespace Assignments.API.Services.UserProfilImage
{
    public class UserProfilImageService : BaseService<UserProfilImageEntity, IUserProfilImageRepository>, IUserProfilImageService
    {
        public UserProfilImageService(IUserProfilImageRepository repository, ILogger<UserProfilImageService> logger) : base(repository, logger)
        {
        }
    }
}
