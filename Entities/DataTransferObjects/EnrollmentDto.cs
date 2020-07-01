using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class EnrollmentDto
    {
        public Guid Id { get; set; }
        public Guid SectionId { get; set; }
        public int Role_Id { get; set; }
    }
}
