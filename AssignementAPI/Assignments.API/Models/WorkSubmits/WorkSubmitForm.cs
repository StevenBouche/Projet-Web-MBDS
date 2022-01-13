using System.Text.Json.Serialization;

namespace Assignments.API.Models.WorkSubmits
{
    public class WorkSubmitForm
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;
        [JsonPropertyName("assignmentId")]
        public int? AssignmentId { get; set; }
    }
}
