using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Domain.Entities
{
    public class Resume : BaseEntity
    {
        public string Title { get; set; }
        public string Summray { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int UserId { get; set; }
        public string Slug { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public GeneratedResumes GeneratedResumes { get; set; }
        public Resume()
        {
            Slug = Guid.NewGuid().ToString();
        }
    }
}
