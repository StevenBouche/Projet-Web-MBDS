using Assignment.DAL.Context;
using Assignment.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Assignment.DAL.Repositories.WorkSubmit
{
    public class WorkSubmitRepository : BaseRepository<WorkSubmitEntity>, IWorkSubmitRepository
    {
        public WorkSubmitRepository(AssignmentContext context, ILogger<WorkSubmitRepository> logger) : base(context, logger)
        {
        }
    }
}
