using Assignments.API.Models.Assignments;
using Assignments.API.Models.Authentification;
using Assignments.API.Models.Search;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.API.Services.Assignments
{
    public interface IAssignmentService : IBaseService<AssignmentEntity>
    {
        Task<Assignment> GetAssignmentByIdAsync(int id);
        Task<Assignment> UpdateAssignmentAsync(AssignmentForm form, UserIdentity identity);
        Task DeleteAssignmentAsync(int id, UserIdentity identity);
        Task<Assignment> CreateAssignmentAsync(AssignmentForm form, UserIdentity identity);
        Task<PaginationResult<Assignment>> GetMyAssignmentsAsync(PaginationForm form, UserIdentity identity);
        Task<PaginationResult<Assignment>> GetAllAssignmentsAsync(PaginationForm form);
    }
}
