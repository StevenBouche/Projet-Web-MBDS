using Assignment.DAL.Context;
using Assignment.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Assignment.DAL.Repositories.UserProfilImage
{
    public class UserProfilImageRepository : BaseRepository<UserProfilImageEntity>, IUserProfilImageRepository
    {
        public UserProfilImageRepository(AssignmentContext context, ILogger<UserProfilImageRepository> logger) : base(context, logger)
        {

        }
    }
}
