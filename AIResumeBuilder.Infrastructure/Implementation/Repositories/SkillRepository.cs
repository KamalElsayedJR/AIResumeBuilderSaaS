using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Domain.Entities;
using AIResumeBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Implementation.Repositories
{
    public class SkillRepository : ISkillRepositroy
    {
        private readonly AppDbContext _dbContext;

        public SkillRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Skill?> GetSkillByIdForReaumAndUserAsync(int SkillId, int ResumeId, int UserId)
        => await _dbContext.Skills.FirstOrDefaultAsync(s => s.Id == SkillId && s.ResumeId == ResumeId && s.Resume.UserId == UserId);
    }
}
