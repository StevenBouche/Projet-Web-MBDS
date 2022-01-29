using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search.Assignments
{
    public class AssignmentsSearchForm
    {
        [JsonPropertyName("courseId")]
        public int? CourseId { get; set; }

        [JsonPropertyName("term")]
        public string Term { get; set; } = string.Empty;
    }
}