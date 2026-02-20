using AIResumeBuilder.Domain.Entities;
using AIResumeBuilder.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(u => u.HashedPassword).IsRequired();
            builder.Property(u => u.Plan).IsRequired().HasConversion(P => P.ToString(), P => (Plan)Enum.Parse(typeof(Plan), P));
            builder.Property(u => u.SubscriptionStatus)
                .IsRequired()
                .HasConversion(SS => SS.ToString(), P => (SubscriptionStatus)Enum.Parse(typeof(SubscriptionStatus), P));
        }
    }
}
