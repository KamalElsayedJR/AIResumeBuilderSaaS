        using AIResumeBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Interfaces.Repositories
{
    public interface IResumeRepository
    {
        public Task<IEnumerable<Resume>> GetByUser(int UserId);
        public Task<Resume> GetByIdAsync(int ResumeId,int UserId);
        public Task<Resume> GetBySlug(string slug);
    }
}
