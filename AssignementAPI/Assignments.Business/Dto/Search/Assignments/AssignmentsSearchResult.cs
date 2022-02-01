using Assignments.Business.Dto.Assignments;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search.Assignments
{
    public class AssignmentsSearchResult
    {
        [JsonPropertyName("courseId")]
        public int? CourseId { get; set; }

        [JsonPropertyName("term")]
        public string Term { get; set; } = string.Empty;

        [JsonPropertyName("results")]
        public List<Assignment> Results { get; set; } = new();
    }
}