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
    public class EventCategoryEntityConfiguration : IEntityTypeConfiguration<EventCategory>
    {
        public void Configure(EntityTypeBuilder<EventCategory> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Id).IsUnique();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(64);
        }
    }
}
