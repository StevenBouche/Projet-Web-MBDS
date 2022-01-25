using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Users
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("picture")]
        public int? PictureId { get; set; } = null;
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;
    }
}
