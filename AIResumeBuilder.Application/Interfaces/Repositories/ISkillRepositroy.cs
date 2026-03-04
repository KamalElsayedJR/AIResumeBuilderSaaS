using AIResumeBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Interfaces.Repositories
{
    public interface ISkillRepositroy
    {
        public Task<Skill?> GetSkillByIdForReaumAndUserAsync(int SkillId,int ResumeId,int UserId);
    }
}
