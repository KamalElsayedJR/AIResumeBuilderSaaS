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
    public class EducationRepository : IEducationRepository
    {
        private readonly AppDbContext _dbContext;

        public EducationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Education?> GetEducationByIdForResumeAndUser(int EducationId, int ResumeId, int UserId)
        => await _dbContext.Educations.FirstOrDefaultAsync(e => e.Id == EducationId && e.ResumeId == ResumeId && e.Resume.UserId == UserId);
        
    }
}
