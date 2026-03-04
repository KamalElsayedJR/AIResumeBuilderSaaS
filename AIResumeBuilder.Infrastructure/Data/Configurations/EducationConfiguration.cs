using AIResumeBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Data.Configurations
{
    public class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.InstitutionName).IsRequired();
            builder.HasOne(e => e.Resume)
                .WithMany(e => e.Educations)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasForeignKey(e => e.ResumeId);
        }
    }
}
