using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Enrollment
    {
        [Column("EnrollmentId")]
        public Guid Id { get; set; }


        [ForeignKey(nameof(Section))]
        public Guid SectionId { get; set; }
        public Section Section { get; set; }


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Range(0, 2)]
        public int Role_Id { get; set; }



        public ICollection<Submission> Submissions { get; set; }

    }
}
