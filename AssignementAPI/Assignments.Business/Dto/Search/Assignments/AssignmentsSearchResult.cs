using Assignments.Business.Dto.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignments.Business.Dto.Search.Assignments
{
    public class AssignmentsSearchResult
    {
        [JsonPropertyName("courseId")]
        public int? CourseId { get; set; }

        [JsonPropertyName("term")]
        public string Term { get; set; } = string.Empty;

        [JsonPropertyName("results")]
        public List<Assignment> Results { get; set; } = new();
    }
}