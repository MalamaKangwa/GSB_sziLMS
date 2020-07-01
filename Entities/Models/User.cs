using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class User
    {
        [Column("UserId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "Last name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Lname { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the email is 100 characters.")]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "minimum length for password is 8 characters.")]
        public string Password { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
