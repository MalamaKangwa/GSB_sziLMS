using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configurations
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasData(
                new Section
                {
                    Id = new Guid("df925c94-fabd-4184-a362-04c97e8ba1cf"),
                    CourseId = new Guid("7ead0595-5f85-4134-b687-b24e3436fd33")
                },
                new Section
                {
                    Id = new Guid("b6aad12f-51fe-4bb5-a60d-0ba22f236ab3 "),
                    CourseId = new Guid("7ead0595-5f85-4134-b687-b24e3436fd33")
                },
                new Section
                {
                    Id = new Guid("af4715dd-331a-4229-8444-d78fddbdb256"),
                    CourseId = new Guid("45c28d41-5c8b-4974-9236-d3776b649295")
                }
            );

        }
    }
}
