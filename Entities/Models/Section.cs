using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Section
    {
        [Column("SectionId")]
        public Guid Id { get; set; }


        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
