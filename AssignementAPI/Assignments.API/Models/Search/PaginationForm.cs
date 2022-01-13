using System.Text.Json.Serialization;

namespace Assignments.API.Models.Search
{
    public class PaginationForm
    {
        [JsonPropertyName("pagesize")]
        public int PageSize { get; set; }
        [JsonPropertyName("page")]
        public int Page { get; set; }
    }
}