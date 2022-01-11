namespace Assignment.DAL.Models
{
    public enum AssignmentState
    {
        OPEN,
        CLOSE
    }

    public class AssignmentEntity : BaseModel
    {
        public string Label { get; set; } = string.Empty;
        public AssignmentState State { get; set; }
        public DateTime DelivryDate { get; set; }
        public virtual CourseEntity Course { get; set; } = new();
        public int CourseId { get; set; }
        public virtual List<WorkSubmitEntity> WorkSubmits { get; set; } = new();
    }
}
