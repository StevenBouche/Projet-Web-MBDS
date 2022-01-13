using Assignments.API.Models.Authentification;
using Assignments.API.Models.Search;
using Assignments.API.Models.WorkSubmits;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.API.Services.WorkSubmits
{
    public interface IWorkSubmitService : IBaseService<WorkSubmitEntity>
    {
        Task<WorkSubmit> GetWorkSubmitByIdAsync(int id);
        Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitForm form, UserIdentity? identity);
        Task DeleteWorkSubmitAsync(int id, UserIdentity? identity);
        Task<WorkSubmit> CreateWorkSubmitAsync(WorkSubmitForm form, UserIdentity? identity);
        Task<PaginationResult<WorkSubmit>> GetMyWorkSubmitsAsync(PaginationForm form, UserIdentity? identity);
        Task<PaginationResult<WorkSubmit>> GetAllWorkSubmitsAsync(PaginationForm form);
    }
}
