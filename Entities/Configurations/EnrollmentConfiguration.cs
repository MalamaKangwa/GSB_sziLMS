using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {

            builder.HasData(
                new Enrollment
                {
                    Id = new Guid("f83d1b8c-a270-4542-bec6-f171f8f3f95c"),
                    SectionId = new Guid("df925c94-fabd-4184-a362-04c97e8ba1cf"),
                    UserId = new Guid("3aaa4e0e-d210-4791-bea4-b847ca737678"),
                    Role_Id = 1
                },
                new Enrollment
                {
                    Id = new Guid("6bdb643b-cf76-4a6b-8941-bed8a02d0cbd"),
                    SectionId = new Guid("df925c94-fabd-4184-a362-04c97e8ba1cf"),
                    UserId = new Guid("e0335745-eb9a-46ca-b70e-fdb0434d54a9"),
                    Role_Id = 1
                },
                new Enrollment
                {
                    Id = new Guid("a3ee8728-a3c9-4ab3-b8a1-ea9bff10ed02"),
                    SectionId = new Guid("df925c94-fabd-4184-a362-04c97e8ba1cf"),
                    UserId = new Guid("622dc736-4634-4db8-8bb7-a33f6359a4be"),
                    Role_Id = 0
                }
            );
        }
    }
}
