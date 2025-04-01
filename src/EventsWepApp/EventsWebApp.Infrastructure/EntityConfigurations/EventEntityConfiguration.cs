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
    internal class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Id).IsUnique();
            builder.HasIndex(e => e.Name).IsUnique();

            builder.HasMany(e => e.Participants)
                .WithOne(p => p.Event)
                .HasForeignKey(p => p.EventId);

            builder.HasOne(e => e.EventCategory)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.EventCategoryId);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);
            builder.Property(e=>e.Description).IsRequired().HasMaxLength(512);
            builder.Property(e=>e.EventDateAndTime).IsRequired();
            builder.Property(e=>e.EventAddress).IsRequired().HasMaxLength(128);
            builder.Property(e=>e.MaxParticipantCount).IsRequired();
        }
    }
}
