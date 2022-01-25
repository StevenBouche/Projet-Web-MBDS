using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search
{
    public class PaginationForm
    {
        [JsonPropertyName("pagesize")]
        public int PageSize { get; set; }
        [JsonPropertyName("page")]
        public int Page { get; set; }
    }
}