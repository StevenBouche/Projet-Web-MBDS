using Assignments.DAL.Enumerations;

namespace Assignments.DAL.Models
{
    public class WorkSubmitEntity : BaseModel
    {
        public string Label { get; set; } = string.Empty;
        public double Grade { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public WorkSubmitState State { get; set; }
        public virtual UserEntity User { get; set; } = new();
        public int UserId { get; set; }
        public virtual AssignmentEntity? Assignment { get; set; } = null;
        public int? AssignmentId { get; set; }
    }
}
