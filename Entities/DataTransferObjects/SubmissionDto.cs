using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class SubmissionDto
    {
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
        public string Score { get; set; }
    }
}
