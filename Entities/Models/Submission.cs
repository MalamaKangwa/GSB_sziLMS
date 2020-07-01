using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Submission
    {
        [Column("SubmissionId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Assignment))]
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }


        [ForeignKey(nameof(Enrollment))]
        public Guid EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }

        [Required(ErrorMessage = "Score is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Score is 60 characters.")]
        public string Score { get; set; }


    }
}