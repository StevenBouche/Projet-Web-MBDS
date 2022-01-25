using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Users
{
    public class UserForm
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;
    }
}
