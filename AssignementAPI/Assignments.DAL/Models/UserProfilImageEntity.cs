namespace Assignments.DAL.Models
{
    public class UserProfilImageEntity : BaseModel
    {
        public string Extention { get; set; } = string.Empty;
        public byte[] Data { get; set; } = Array.Empty<byte>();
        public virtual UserEntity? User { get; set; } = null;
        public int UserId { get; set; }
    }
}
