namespace Assignments.DAL.Models
{
    public class CourseEntity : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual UserEntity User { get; set; }
        public int UserId { get; set; }
        public virtual CourseImageEntity? Image { get; set; } = null;
        public int? ImageId { get; set; }
        public virtual List<AssignmentEntity> Assignments { get; set; } = new();
    }
}