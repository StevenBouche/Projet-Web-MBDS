
namespace Assignment.DAL.Models
{
    public class RefreshTokenEntity : BaseModel
    {
        public string Token { get; set; } = string.Empty;
        public long ExpireAt { get; set; }
        public virtual UserEntity? User { get; set; } = null;
        public int UserId { get; set; }
    }
}
