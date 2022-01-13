using Assignments.DAL.Context;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Assignments.DAL.Repositories.WorkSubmits
{
    public class WorkSubmitRepository : BaseRepository<WorkSubmitEntity>, IWorkSubmitRepository
    {
        public WorkSubmitRepository(AssignmentContext context, ILogger<WorkSubmitRepository> logger) : base(context, logger)
        {
        }
    }
}
