using EventsWebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Infrastructure.EntityConfigurations
{
    internal class UserEntitiyConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u=>u.Id).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(64);
            builder.Property(u=>u.LastName).IsRequired().HasMaxLength(64);
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Role).IsRequired();
            builder.Property(u=>u.DateOfBirth).IsRequired();
        }
    }
}
