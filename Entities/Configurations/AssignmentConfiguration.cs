using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasData(
                new Assignment
                {
                    Id = new Guid("e0e491c5-83a7-4c22-a86d-e421eac33aaa"),
                    Assignment_Name = "Criminology Task 1",
                    Assignment_Description = "Define Criminal Behaviour",
                    CourseId = new Guid("7ead0595-5f85-4134-b687-b24e3436fd33")
                },
                new Assignment
                {
                    Id = new Guid("ad44f581-ee0e-4266-94e7-4892196ad9cc"),
                    Assignment_Name = "Criminology Task 2",
                    Assignment_Description = "List 10 Criminology Methodologies",
                    CourseId = new Guid("7ead0595-5f85-4134-b687-b24e3436fd33")
                }
            );
        }
    }
}
