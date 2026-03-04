using AIResumeBuilder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Dtos.Resume.Skill
{
    public class AddSkillDto
    {
        public string SkillName { get; set; }
        public SkillCategory Category { get; set; }
    }
}
