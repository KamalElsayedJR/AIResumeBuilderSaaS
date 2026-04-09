using AIResumeBuilder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Data.Configurations
{
    public class GeneratedResumesConfiguration : IEntityTypeConfiguration<GeneratedResumes>
    {
        public void Configure(EntityTypeBuilder<GeneratedResumes> builder)
        {
            builder.HasKey(gr => gr.Id);
            builder.Property(gr => gr.Content).IsRequired();
            builder.Property(gr => gr.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.HasOne(gr => gr.Resume).WithOne(r => r.GeneratedResumes).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(gr=>gr.User).WithMany(u=>u.GeneratedResumes).HasForeignKey(gr=>gr.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
