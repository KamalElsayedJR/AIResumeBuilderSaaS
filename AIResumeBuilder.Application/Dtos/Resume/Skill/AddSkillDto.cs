using AIResumeBuilder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Dtos.Resume.Skill
{
    public class AddSkillDto
    {
        [Required]
        public string SkillName { get; set; }
        [Required]
        public SkillCategory Category { get; set; }
    }
}
