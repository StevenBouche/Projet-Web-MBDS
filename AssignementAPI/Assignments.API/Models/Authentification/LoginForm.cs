using System.Text.Json.Serialization;

namespace Assignments.API.Models.Authentification
{
    public class LoginForm
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
