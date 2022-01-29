using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search.Courses
{
    public class CoursesSearchForm
    {
        [JsonPropertyName("term")]
        public string Term { get; set; } = string.Empty;
    }
}