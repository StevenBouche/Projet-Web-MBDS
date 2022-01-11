using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DAL.Models
{
    public enum WorkSubmitState
    {
        CREATED,
        SUBMITTED,
        EVALUATED
    }

    public class WorkSubmitEntity : BaseModel
    {
        public string Label { get; set; } = string.Empty;
        public double Grade { get; set; }
        public string Comment { get; set; } = string.Empty;
        public WorkSubmitState State { get; set; }
        public virtual UserEntity User { get; set; } = new();
        public int UserId { get; set; }
        public virtual AssignmentEntity? Assignment { get; set; } = null;
        public int? AssignmentId { get; set; }
    }
}
