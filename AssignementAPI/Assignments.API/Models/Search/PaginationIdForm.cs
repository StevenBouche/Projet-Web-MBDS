using System.Text.Json.Serialization;

namespace Assignments.API.Models.Search
{
    public class PaginationIdForm : PaginationForm
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
