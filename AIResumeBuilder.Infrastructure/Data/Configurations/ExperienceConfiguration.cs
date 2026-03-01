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
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.JobTitle)
                .IsRequired();
            builder.Property(e => e.StartDate)
                .IsRequired();
            builder.Property(e => e.CompanyName).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.HasOne(e=>e.Resume).WithMany(r=>r.Experiences)
                .HasForeignKey(e=>e.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(e=>e.EmploymentType).IsRequired(false).HasConversion(et=>et.ToString(),et=> (EmploymentType)Enum.Parse(typeof(EmploymentType),et));

        }
    }
}
