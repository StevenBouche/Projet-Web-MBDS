using Assignments.Business.Dto.Users;
using Assignments.DAL.Models;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Courses
{
    public class Course
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("pictureId")]
        public int? PictureId { get; set; }

        [JsonPropertyName("user")]
        public User? User { get; set; }

        [JsonPropertyName("stats")]
        public CourseStats? Stats { get; set; } = null;

        [JsonPropertyName("createAt")]
        public DateTimeOffset CreateAt { get; set; }

        [JsonPropertyName("updateAt")]
        public DateTimeOffset UpdateAt { get; set; }
    }
}