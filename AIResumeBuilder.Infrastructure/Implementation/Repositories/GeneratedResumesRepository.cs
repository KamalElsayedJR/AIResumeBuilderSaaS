using AIResumeBuilder.Application.Dtos.AI;
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
    public class GeneratedResumesRepository : IGeneratedResumesRepository
    {
        private readonly AppDbContext _dbContext;

        public GeneratedResumesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountThisMonthAsync(int UserId)
        {
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            return await _dbContext.GeneratedResumes
                .Where(x => x.UserId == UserId && x.CreatedAt >= startOfMonth)
                .CountAsync();
        }
        public async Task<GeneratedResumes?> GetGeneratedResumesAsync(int resumeId,int UserId)
        => await _dbContext.GeneratedResumes.Where(x => x.ResumeId == resumeId && x.UserId == UserId).FirstOrDefaultAsync();
        
    }
}
