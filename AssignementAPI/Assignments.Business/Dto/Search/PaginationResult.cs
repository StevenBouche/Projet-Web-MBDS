using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search
{
    public class PaginationResult<T>
    {
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("totalPage")]
        public int TotalPage { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
        [JsonPropertyName("results")]
        public List<T> Results { get; set; } = new List<T>();
    }
}
