using AIResumeBuilder.Application.Dtos.Resume.Education;
using AIResumeBuilder.Application.Dtos.Resume.Experience;
using AIResumeBuilder.Application.Dtos.Resume.Skill;
using AIResumeBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Dtos.Resume
{
    public class ResumeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summray { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<ExperienceDto> Experiences { get; set; }
        public ICollection<SkillDto> Skills { get; set; }
        public ICollection<EducationDto> Educations { get; set; }
    }
}
