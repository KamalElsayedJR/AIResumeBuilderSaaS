using AIResumeBuilder.Domain.Entities;
using AIResumeBuilder.Domain.Enums;
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
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(s=>s.Resume)
                .WithMany(s=>s.Skills)
                .HasForeignKey(s=>s.ResumeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(s=>s.SkillName).IsRequired();
            builder.Property(s=>s.Category).IsRequired();
            builder.Property(s => s.Category).IsRequired().HasConversion(c=>c.ToString(),c=> (SkillCategory)Enum.Parse(typeof(SkillCategory) ,c));
        }
    }
}
