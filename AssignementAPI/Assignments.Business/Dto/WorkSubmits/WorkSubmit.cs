using Assignments.Business.Dto.Assignments;
using Assignments.Business.Dto.Users;
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

        [JsonPropertyName("stateLabel")]
        public string StateLabel { get => this.State.ToString(); }

        [JsonPropertyName("submittedDate")]
        public DateTime? SubmittedDate { get; set; }

        [JsonPropertyName("assignment")]
        public Assignment? Assignment { get; set; }

        [JsonPropertyName("user")]
        public User? User { get; set; }

        [JsonPropertyName("isLate")]
        public bool IsLate { get; set; }
    }
}