using Assignments.DAL.Enumerations;

namespace Assignments.DAL.Models
{
    public class UserEntity : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRoles Role { get; set; } = UserRoles.NONE;
        public virtual UserProfilImageEntity? Image { get; set; } = null;
        public int? ImageId { get; set; }
        public virtual RefreshTokenEntity? RefreshToken { get; set; } = null;
        public int? RefreshTokenId { get; set; }
        public virtual List<WorkSubmitEntity> WorkSubmits { get; set; } = new();
        public virtual List<CourseEntity> Courses { get; set; } = new();
    }
}
