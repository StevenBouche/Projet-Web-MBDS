using Assignments.DAL.Context;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Assignments.DAL.Repositories.Assignments
{
    public class AssignmentRepository : BaseRepository<AssignmentEntity>, IAssignmentRepository
    {
        public AssignmentRepository(AssignmentContext context, ILogger<AssignmentRepository> logger) : base(context, logger)
        {

        }
    }
}
