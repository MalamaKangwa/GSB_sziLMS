using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = new Guid("3aaa4e0e-d210-4791-bea4-b847ca737678"),
                    Fname = "James",
                    Lname = "Jamerson",
                    Email = "JJ@gmail.com",
                    Password = "weonder2039"
                },
                new User
                {
                    Id = new Guid("e0335745-eb9a-46ca-b70e-fdb0434d54a9"),
                    Fname = "Karen",
                    Lname = "Killian",
                    Email = "KK@gmail.com",
                    Password = "darir7079"
                },
                new User
                {
                    Id = new Guid("622dc736-4634-4db8-8bb7-a33f6359a4be"),
                    Fname = "Lama",
                    Lname = "Lambada",
                    Email = "LL@gmail.com",
                    Password = "weonder2039"
                });
        }
    }
}
