using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    Id = new Guid("7ead0595-5f85-4134-b687-b24e3436fd33"),
                    Course_Name = "Criminology 103",
                    Course_Description = "Criminal Methodologies"

                },
                new Course
                {
                    Id = new Guid("45c28d41-5c8b-4974-9236-d3776b649295"),
                    Course_Name = "Water Analysis",
                    Course_Description = "Best Practices for Requirements Gathering."
                }

            );

        }
    }
}
