using AIResumeBuilder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Domain.Entities
{
    public class Skill : BaseEntity
    {
        public string SkillName { get; set; }
        public SkillCategory Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
