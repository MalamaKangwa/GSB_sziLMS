using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Course
    {
        [Column("CourseId")]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Course name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Course_Name { get; set; }

        [Required(ErrorMessage = "Course Description is a required field.")]
        [MaxLength(200, ErrorMessage = "Maximum length for the description is 200 characte")]
        public string Course_Description { get; set; }

        public ICollection<Section> Sections { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

    }
}
