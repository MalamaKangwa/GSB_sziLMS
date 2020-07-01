using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configurations
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {

            builder.HasOne(b => b.Enrollment)
                .WithMany(a => a.Submissions)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasData(
                new Submission
                {
                    Id = new Guid("0b7c5236-9442-4fda-a5db-d621277c5f61"),
                    AssignmentId = new Guid("e0e491c5-83a7-4c22-a86d-e421eac33aaa"),
                    EnrollmentId = new Guid("f83d1b8c-a270-4542-bec6-f171f8f3f95c"),
                    Score = "76%"

                },
                new Submission
                {
                    Id = new Guid("55863227-033c-4056-9a89-4fe5a78abe52"),
                    AssignmentId = new Guid("ad44f581-ee0e-4266-94e7-4892196ad9cc"),
                    EnrollmentId = new Guid("f83d1b8c-a270-4542-bec6-f171f8f3f95c"),
                    Score = "80%"

                }
            );
        }
    }
}
