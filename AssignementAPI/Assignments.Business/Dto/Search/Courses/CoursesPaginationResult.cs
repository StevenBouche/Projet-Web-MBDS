using Assignments.Business.Dto.Courses;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search.Courses
{
    public class CoursesPaginationResult : PaginationResult<Course>
    {
        [JsonPropertyName("courseName")]
        public string CourseName { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
    }
}