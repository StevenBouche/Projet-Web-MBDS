using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Courses
{
    public class CourseFormCreate
    {
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
