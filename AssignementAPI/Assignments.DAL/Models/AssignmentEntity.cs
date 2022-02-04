using Assignments.DAL.Enumerations;

namespace Assignments.DAL.Models
{
    public class AssignmentEntity : BaseModel
    {
        public string Label { get; set; } = string.Empty;
        public AssignmentState State { get; set; }
        public DateTimeOffset DelivryDate { get; set; }
        public virtual CourseEntity? Course { get; set; } = null;
        public int CourseId { get; set; }
        public virtual List<WorkSubmitEntity> WorkSubmits { get; set; } = new();
    }
}