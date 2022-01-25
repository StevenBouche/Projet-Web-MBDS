using Assignments.DAL.Enumerations;
using Assignments.DAL.Models;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.WorkSubmits
{
    public class WorkSubmit
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        [JsonPropertyName("grade")]
        public double Grade { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;
        [JsonPropertyName("state")]
        public WorkSubmitState State { get; set; }
        [JsonPropertyName("assignmentId")]
        public int? AssignmentId { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}
