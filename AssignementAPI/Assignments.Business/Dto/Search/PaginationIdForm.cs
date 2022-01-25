using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search
{
    public class PaginationIdForm : PaginationForm
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
