using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Assignments
{
    public class AssignmentForm
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        [JsonPropertyName("delivryDate")]
        public DateTime DelivryDate { get; set; }
        [JsonPropertyName("courseId")]
        public int CourseId { get; set; }
    }
}
