using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search.Courses
{
    public class CoursesPaginationForm : PaginationForm
    {
        [JsonPropertyName("userId")]
        public int? UserId { get; set; } = null;
        [JsonPropertyName("courseName")]
        public string CourseName { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
    }
}