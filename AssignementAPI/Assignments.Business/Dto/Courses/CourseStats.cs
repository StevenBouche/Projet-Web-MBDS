using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Courses
{
    public class CourseStats
    {
        [JsonPropertyName("totalworks")]
        public int TotalWorks { get; set; }
        [JsonPropertyName("totalassignments")]
        public int TotalAssignments { get; set; }
    }
}