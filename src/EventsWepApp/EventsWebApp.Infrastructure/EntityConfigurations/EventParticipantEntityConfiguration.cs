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
    internal class EventParticipantEntityConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Id).IsUnique();

            builder.HasOne(p => p.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(p => p.EventId);

            builder.HasOne(p=>p.User)
                .WithMany(u=>u.EventParticipations)
                .HasForeignKey(p => p.UserId);

            builder.Property(p => p.RegistrationDate).IsRequired();
        }
    }
}
