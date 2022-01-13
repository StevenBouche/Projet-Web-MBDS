using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignments.API.Models.Courses
{
    public class CourseForm
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; } = null;
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
