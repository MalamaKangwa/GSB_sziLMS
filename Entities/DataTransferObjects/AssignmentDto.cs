using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class AssignmentDto
    {
        public Guid Id { get; set; }
        public string Assignment_Name { get; set; }
        public string Assignment_Description { get; set; }
    }
}
