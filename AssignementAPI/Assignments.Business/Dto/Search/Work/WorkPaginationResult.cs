using Assignments.Business.Dto.WorkSubmits;
using Assignments.DAL.Enumerations;
using System.Text.Json.Serialization;

namespace Assignments.Business.Dto.Search.Work
{
    public class WorkPaginationResult : PaginationResult<WorkSubmit>
    {
        [JsonPropertyName("assignmentId")]
        public int AssignmentId { get; set; }

        [JsonPropertyName("state")]
        public WorkSubmitState State { get; set; }
    }
}