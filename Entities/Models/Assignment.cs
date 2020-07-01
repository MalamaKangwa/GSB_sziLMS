using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Assignment
    {
        [Column("AssignmentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Assignment name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Assignment_Name { get; set; }

        [Required(ErrorMessage = "Assignment Description is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the descrption is 60 characte")]
        public string Assignment_Description { get; set; }


        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Submission> Submissions { get; set; }
    }
}
