using Assignment.DAL.Models;
using System.Text.Json.Serialization;

namespace AssignmentAPI.Models.Assignments
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
