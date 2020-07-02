using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjectsForCreation
{
    public class AssignmentForCreationDto
    {
        [Required(ErrorMessage = "Assignment name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Assignment_Name { get; set; }

        [Required(ErrorMessage = "Assignment Description is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the descrption is 60 characte")]
        public string Assignment_Description { get; set; }

        public IEnumerable<SubmissionForCreationDto> Submissions { get; set; }
    }
}

