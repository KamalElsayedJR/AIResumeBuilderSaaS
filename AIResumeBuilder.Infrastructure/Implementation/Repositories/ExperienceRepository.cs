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
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly AppDbContext _dbContext;

        public ExperienceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Experience> GetExperienceByIdForSpecificResumeAsync(int ExperienceId, int ResumeId, int UserId)
        => await _dbContext.Experiences.FirstOrDefaultAsync(e => e.Id == ExperienceId && e.ResumeId == ResumeId && e.Resume.UserId == UserId);
        
    }
}
