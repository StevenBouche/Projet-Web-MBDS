using Assignments.API.Models.Authentification;
using Assignments.API.Models.Search;
using Assignments.API.Models.WorkSubmits;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.WorkSubmits;

namespace Assignments.API.Services.WorkSubmits
{
    public class WorkSubmitService : BaseService<WorkSubmitEntity, IWorkSubmitRepository>, IWorkSubmitService
    {
        public WorkSubmitService(IWorkSubmitRepository repository, ILogger<WorkSubmitService> logger) : base(repository, logger)
        {

        }

        Task<WorkSubmit> IWorkSubmitService.CreateWorkSubmitAsync(WorkSubmitForm form, UserIdentity? identity)
        {
            throw new NotImplementedException();
        }

        Task IWorkSubmitService.DeleteWorkSubmitAsync(int id, UserIdentity? identity)
        {
            throw new NotImplementedException();
        }

        Task<PaginationResult<WorkSubmit>> IWorkSubmitService.GetAllWorkSubmitsAsync(PaginationForm form)
        {
            throw new NotImplementedException();
        }

        Task<PaginationResult<WorkSubmit>> IWorkSubmitService.GetMyWorkSubmitsAsync(PaginationForm form, UserIdentity? identity)
        {
            throw new NotImplementedException();
        }

        Task<WorkSubmit> IWorkSubmitService.GetWorkSubmitByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<WorkSubmit> IWorkSubmitService.UpdateWorkSubmitAsync(WorkSubmitForm form, UserIdentity? identity)
        {
            throw new NotImplementedException();
        }
    }
}
