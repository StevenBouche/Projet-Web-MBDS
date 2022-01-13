using Assignments.API.Models.Assignments;
using Assignments.API.Models.Authentification;
using Assignments.API.Models.Search;
using Assignments.API.Services.Base;
using Assignments.DAL.Models;
using Assignments.DAL.Repositories.Assignments;

namespace Assignments.API.Services.Assignments
{
    public class AssignmentService : BaseService<AssignmentEntity, IAssignmentRepository>, IAssignmentService
    {
        public AssignmentService(IAssignmentRepository repository, ILogger<AssignmentService> logger) : base(repository, logger)
        {
        }

        public Task<Assignment> CreateAssignmentAsync(AssignmentForm form, UserIdentity identity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAssignmentAsync(int id, UserIdentity identity)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationResult<Assignment>> GetAllAssignmentsAsync(PaginationForm form)
        {
            throw new NotImplementedException();
        }

        public Task<Assignment> GetAssignmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationResult<Assignment>> GetMyAssignmentsAsync(PaginationForm form, UserIdentity identity)
        {
            throw new NotImplementedException();
        }

        public Task<Assignment> UpdateAssignmentAsync(AssignmentForm form, UserIdentity identity)
        {
            throw new NotImplementedException();
        }
    }
}
