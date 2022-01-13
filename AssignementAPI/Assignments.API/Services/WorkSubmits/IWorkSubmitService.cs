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
        Task DeleteWorkSubmitAsync(int id, UserIdentity identity);
        Task<WorkSubmit> CreateWorkSubmitAsync(WorkSubmitStudentForm form, UserIdentity identity);
        Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitStudentForm form, UserIdentity identity);
        Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitProfessorForm form, UserIdentity identity);
        Task<WorkSubmit> SubmitWorkSubmitAsync(WorkSubmitActionForm form, UserIdentity identity);
        Task<WorkSubmit> EvaluateWorkSubmitAsync(WorkSubmitActionForm form, UserIdentity identity);
        Task<PaginationResult<WorkSubmit>> GetMyWorkSubmitsAsync(PaginationForm form, UserIdentity identity);
        Task<PaginationResult<WorkSubmit>> GetAllWorkSubmitsAsync(PaginationForm form);
        Task<PaginationResult<WorkSubmit>> GetAllSubmitsOfAssignmentAsync(PaginationIdForm form);
    }
}
