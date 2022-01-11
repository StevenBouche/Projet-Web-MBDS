using Assignment.DAL.Context;
using Assignment.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Assignment.DAL.Repositories.Assignment
{
    public class AssignmentRepository : BaseRepository<AssignmentEntity>, IAssignmentRepository
    {
        public AssignmentRepository(AssignmentContext context, ILogger<AssignmentRepository> logger) : base(context, logger)
        {

        }
    }
}
