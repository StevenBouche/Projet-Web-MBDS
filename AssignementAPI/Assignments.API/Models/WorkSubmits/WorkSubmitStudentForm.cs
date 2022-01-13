using System.Text.Json.Serialization;

namespace Assignments.API.Models.WorkSubmits
{
    public class WorkSubmitStudentForm
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; } = null;
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("assignmentId")]
        public int? AssignmentId { get; set; } = null;
    }
}
