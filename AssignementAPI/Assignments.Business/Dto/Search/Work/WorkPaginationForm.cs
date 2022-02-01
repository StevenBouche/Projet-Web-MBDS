using Assignments.DAL.Enumerations;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search.Work
{
    public class WorkPaginationForm : PaginationForm
    {
        [JsonPropertyName("assignmentId")]
        public int AssignmentId { get; set; }

        [JsonPropertyName("state")]
        public WorkSubmitState State { get; set; }
    }
}