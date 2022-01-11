using Assignment.DAL.Models;
using Assignment.DAL.Repositories.WorkSubmit;

namespace AssignmentAPI.Services.WorkSubmit
{
    public class WorkSubmitService : BaseService<WorkSubmitEntity, IWorkSubmitRepository>, IWorkSubmitService
    {
        public WorkSubmitService(IWorkSubmitRepository repository, ILogger<WorkSubmitService> logger) : base(repository, logger)
        {

        }
    }
}
