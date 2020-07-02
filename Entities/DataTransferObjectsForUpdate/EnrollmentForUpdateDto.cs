using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjectsForUpdate
{
    public class EnrollmentForUpdateDto
    {
        public Guid SectionId { get; set; }

        [Range(0, 2)]
        public int Role_Id { get; set; }
    }
}
