using Assignments.Business.Dto.Assignments;
using Assignments.Business.Dto.Search;
using Assignments.Business.Dto.WorkSubmits;
using Assignments.Business.Services.Base;
using Assignments.DAL.Models;

namespace Assignments.Business.Services.Assignments
{
    public interface IAssignmentService : IBaseService<AssignmentEntity>
    {
        Task<Assignment> GetAssignmentByIdAsync(int? id);

        Task<Assignment> UpdateAssignmentAsync(AssignmentForm form);

        Task DeleteAssignmentAsync(int id);

        Task<Assignment> CreateAssignmentAsync(AssignmentForm form);

        Task<Assignment> OpenAssignmentAsync(int? id);

        Task<Assignment> CloseAssignmentAsync(int? id);

        Task<PaginationResult<Assignment>> GetAllAssignmentsAsync(PaginationForm form);

        Task<PaginationResult<Assignment>> GetAllAssignmentsOfCourseAsync(int course, PaginationForm form);

        Task<PaginationResult<WorkSubmit>> GetAllWorksAssignmentAsync(int id, PaginationForm form);
    }
}