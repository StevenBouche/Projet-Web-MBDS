using System.Text.Json.Serialization;

namespace AssignmentAPI.Models.User
{
    public class RegisterView
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
