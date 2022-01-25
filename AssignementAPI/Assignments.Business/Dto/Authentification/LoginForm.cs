using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Authentification
{
    public class LoginForm
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
