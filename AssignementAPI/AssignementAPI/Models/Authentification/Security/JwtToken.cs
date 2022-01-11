using System.Text.Json.Serialization;

namespace AssignmentAPI.Models.Authentification.Security
{
    public class JwtToken
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = String.Empty;

        [JsonPropertyName("expireAt")]
        public long ExpireAt { get; set; }
    }
}
