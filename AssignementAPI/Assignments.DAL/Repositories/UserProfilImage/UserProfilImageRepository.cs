using Assignments.DAL.Context;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Assignments.DAL.Repositories.UserProfilImage
{
    public class UserProfilImageRepository : BaseRepository<UserProfilImageEntity>, IUserProfilImageRepository
    {
        public UserProfilImageRepository(AssignmentContext context, ILogger<UserProfilImageRepository> logger) : base(context, logger)
        {

        }
    }
}
