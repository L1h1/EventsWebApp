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
    public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.Id);

            builder.HasOne(r => r.User)
                .WithMany().HasForeignKey(r => r.UserId);

            builder.Property(r => r.Token).IsRequired().HasMaxLength(200);
            builder.Property(r => r.ExpiresOn).IsRequired();
        }
    }
}
