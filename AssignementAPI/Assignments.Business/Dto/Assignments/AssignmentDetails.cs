using Assignments.DAL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignments.Business.Dto.Assignments
{
    public class AssignmentDetails
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
        public DateTime DelivryDate { get; set; }

        [JsonPropertyName("courseId")]
        public int CourseId { get; set; }

        [JsonPropertyName("courseName")]
        public string CourseName { get; set; } = string.Empty;

        [JsonPropertyName("courseDescription")]
        public string CourseDescription { get; set; } = string.Empty;

        [JsonPropertyName("coursePictureId")]
        public int? CoursePictureId { get; set; }
    }
}