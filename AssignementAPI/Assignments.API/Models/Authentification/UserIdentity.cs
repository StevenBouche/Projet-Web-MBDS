using Assignments.DAL.Models;
using System.Security.Claims;

namespace Assignments.API.Models.Authentification
{
    public class UserIdentity
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? PictureId { get; set; }

        public UserIdentity(UserEntity userAccount)
        {
            Id = userAccount.Id;
            Role = userAccount.Role.ToString();
            Name = userAccount.Name;
            PictureId = userAccount.ImageId;
        }

        public UserIdentity(ClaimsPrincipal claims)
        {
            Id = int.Parse(claims.FindFirstValue(ClaimTypes.NameIdentifier));
            Role = claims.FindFirstValue(ClaimTypes.Role);
            Name = claims.FindFirstValue(ClaimTypes.Surname);
            var pid = claims.FindFirstValue("UrlPicture");
            PictureId = string.IsNullOrWhiteSpace(pid) ? null : int.Parse(pid);
        }

        public Claim[] GetClaims()
        {

            var pid = PictureId.ToString();
            return new[]
            {
                new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                new Claim(ClaimTypes.Role, Role),
                new Claim(ClaimTypes.Surname, Name),
                new Claim("UrlPicture", pid ?? string.Empty)
            };
        }
    }
}
