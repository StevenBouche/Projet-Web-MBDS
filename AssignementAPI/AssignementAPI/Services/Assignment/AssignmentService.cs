using Assignment.DAL.Models;
using Assignment.DAL.Repositories.Assignment;

namespace AssignmentAPI.Services.Assignment
{
    public class AssignmentService : BaseService<AssignmentEntity, IAssignmentRepository>, IAssignmentService
    {
        public AssignmentService(IAssignmentRepository repository, ILogger<AssignmentService> logger) : base(repository, logger)
        {
        }
    }
}
