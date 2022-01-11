namespace Assignment.DAL.Models
{
    public class UserProfilImageEntity : BaseModel
    {
        public string Extention { get; set; } = string.Empty;
        public byte[] Data { get; set; } = Array.Empty<byte>();
        public virtual UserEntity User { get; set; } = new UserEntity();
        public int UserId { get; set; }
    }
}
