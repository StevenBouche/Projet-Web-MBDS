using Assignments.Business.Dto.Courses;
using Assignments.DAL.Enumerations;
using Assignments.DAL.Models;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Assignments
{
    public class Assignment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        [JsonPropertyName("state")]
        public AssignmentState State { get; set; }

        [JsonPropertyName("stateLabel")]
        public string StateLabel { get => this.State.ToString(); }

        [JsonPropertyName("delivryDate")]
        public DateTimeOffset DelivryDate { get; set; }

        [JsonPropertyName("deliveryDateLabel")]
        public string DeliveryDateLabel { get; set; }

        [JsonPropertyName("course")]
        public Course? Course { get; set; }

        [JsonPropertyName("haveWork")]
        public bool HaveWork { get; set; }

        [JsonPropertyName("createAt")]
        public DateTimeOffset CreateAt { get; set; }

        [JsonPropertyName("updateAt")]
        public DateTimeOffset UpdateAt { get; set; }

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