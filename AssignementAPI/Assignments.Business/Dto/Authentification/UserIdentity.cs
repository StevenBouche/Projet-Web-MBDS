using Assignments.DAL.Models;
using System.Security.Claims;

namespace Assignments.Business.Dto.Authentification
{
    public class UserIdentity
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? PictureId { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public UserIdentity()
        {
        }

        public UserIdentity(UserEntity userAccount)
        {
            Id = userAccount.Id;
            Role = userAccount.Role.ToString();
            Name = userAccount.Name;
            PictureId = userAccount.Image?.Id ?? null;
        }

        public UserIdentity(ClaimsPrincipal claims)
        {
            var claimId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            var pid = claims.FindFirstValue("UrlPicture");

            Id = claimId != null ? int.Parse(claimId) : default;
            Role = claims.FindFirstValue(ClaimTypes.Role) ?? string.Empty;
            Name = claims.FindFirstValue(ClaimTypes.Surname) ?? string.Empty;
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