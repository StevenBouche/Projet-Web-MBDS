using Assignment.DAL.Models;
using System.Text.Json.Serialization;

namespace AssignmentAPI.Models.User
{
    public class UserView
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
