using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.WorkSubmits
{
    public class WorkSubmitActionForm
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
