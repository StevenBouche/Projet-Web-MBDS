using System.Text.Json.Serialization;

namespace Assignments.API.Models.Authentification.Tokens
{
    public class JwtToken
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("expireAt")]
        public long ExpireAt { get; set; }
    }
}
