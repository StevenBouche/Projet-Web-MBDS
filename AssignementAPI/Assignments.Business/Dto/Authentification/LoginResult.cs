using Assignments.Business.Dto.Authentification.Tokens;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Authentification
{
    public class LoginResult
    {
        [JsonPropertyName("jwtToken")]
        public JwtToken? JwtToken { get; set; } = null;
        [JsonPropertyName("refreshToken")]
        public RefreshToken? RefreshToken { get; set; } = null;
    }
}
