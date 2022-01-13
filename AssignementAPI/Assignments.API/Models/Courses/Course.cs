using Assignments.DAL.Models;
using System.Text.Json.Serialization;

namespace Assignments.API.Models.Courses
{
    public class Course
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("pictureId")]
        public int? PictureId { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}
