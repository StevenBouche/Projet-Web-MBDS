using Assignment.DAL.Models;
using Assignment.DAL.Repositories.UserProfilImage;

namespace AssignmentAPI.Services.UserProfilImage
{
    public class UserProfilImageService : BaseService<UserProfilImageEntity, IUserProfilImageRepository>, IUserProfilImageService
    {
        public UserProfilImageService(IUserProfilImageRepository repository, ILogger<UserProfilImageService> logger) : base(repository, logger)
        {
        }
    }
}
