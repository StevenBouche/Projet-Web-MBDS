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
        Task DeleteWorkSubmitAsync(int id);
        Task<WorkSubmit> CreateWorkSubmitAsync(WorkSubmitStudentForm form);
        Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitStudentForm form);
        Task<WorkSubmit> UpdateWorkSubmitAsync(WorkSubmitProfessorForm form);
        Task<WorkSubmit> SubmitWorkSubmitAsync(WorkSubmitActionForm form);
        Task<WorkSubmit> EvaluateWorkSubmitAsync(WorkSubmitActionForm form);
        Task<PaginationResult<WorkSubmit>> GetAllWorkSubmitsAsync(PaginationForm form);
        Task<PaginationResult<WorkSubmit>> GetAllSubmitsOfAssignmentAsync(PaginationIdForm form);
        Task<PaginationResult<WorkSubmit>> GetAllWorksAssignmentAsync(int id, PaginationForm form);
    }
}
