using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Courses
{
    public class CourseFormUpdate
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
