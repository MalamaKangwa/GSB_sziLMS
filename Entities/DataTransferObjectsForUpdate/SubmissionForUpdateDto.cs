using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjectsForUpdate
{
    public class SubmissionForUpdateDto
    {
        public Guid EnrollmentId { get; set; }

        [Required(ErrorMessage = "Score is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Score is 60 characters.")]

        public string Score { get; set; }
    }
}

