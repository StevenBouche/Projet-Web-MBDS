using Assignments.DAL.Enumerations;
using Assignments.DAL.Models;
using System.Text.Json.Serialization;

namespace Assignments.API.Models.Assignments
{
    public class Assignment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        [JsonPropertyName("state")]
        public AssignmentState State { get; set; }
        [JsonPropertyName("delivryDate")]
        public DateTime DelivryDate { get; set; }
        [JsonPropertyName("courseId")]
        public int? CourseId { get; set; }

        public Assignment()
        {

        }

        public Assignment(AssignmentEntity entity)
        {
            Id = entity.Id;
            Label = entity.Label;
            State = entity.State;
            DelivryDate = entity.DelivryDate;
        }

    }
}
