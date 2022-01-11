using System.Text.Json.Serialization;

namespace AssignmentAPI.Models.Authentification.View
{
    public class LoginView
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
