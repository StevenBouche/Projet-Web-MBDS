using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignments.Business.Dto.Tree
{
    public class CourseTreeNode
    {
        public int Id { get; set; }
        public string IdName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string WorkName { get; set; } = string.Empty;
        public double? Grade { get; set; }
        public string? StateAssignment { get; set; } = string.Empty;
        public string? StateWork { get; set; } = string.Empty;
        public DateTimeOffset? DeliveryDate { get; set; }
        public DateTimeOffset? SubmittedDate { get; set; }
        public IList<CourseTreeNode>? Children { get; set; }
    }
}