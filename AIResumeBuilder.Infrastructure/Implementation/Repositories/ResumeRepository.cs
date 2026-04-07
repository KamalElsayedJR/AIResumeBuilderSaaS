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
    public class ResumeRepository : IResumeRepository
    {
        private readonly AppDbContext _dbContext;
        public ResumeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Resume> GetByIdAsync(int ResumeId, int UserId)
        => await _dbContext.Resumes.Include(ex=>ex.Experiences).Include(ex => ex.Skills).Include(ex => ex.Educations).FirstOrDefaultAsync(r => r.Id == ResumeId && r.UserId == UserId&&r.IsDeleted==false);
        public async Task<IEnumerable<Resume>> GetByUser(int UserId)
        => await _dbContext.Resumes.Include(ex => ex.Experiences).Include(ex => ex.Skills).Include(ex => ex.Educations).Where(r => r.UserId == UserId).ToListAsync();       
        public async Task<Resume> GetBySlug(string slug)
        => await _dbContext.Resumes.Include(ex => ex.Experiences).Include(ex => ex.Skills).Include(ex => ex.Educations).FirstOrDefaultAsync(r => r.Slug == slug);
    }
}
