using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.WorkSubmits
{
    public class WorkSubmitProfessorForm
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("grade")]
        public double Grade { get; set; }
        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;
    }
}
