using System.Text.Json.Serialization;

namespace Assignments.API.Models.WorkSubmits
{
    public class WorkSubmitActionForm
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
