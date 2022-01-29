using Assignments.Business.Dto.Courses;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search.Courses
{
    public class CoursesSearchResult
    {
        [JsonPropertyName("term")]
        public string Term { get; set; } = string.Empty;

        [JsonPropertyName("results")]
        public List<Course> Results { get; set; } = new();
    }
}