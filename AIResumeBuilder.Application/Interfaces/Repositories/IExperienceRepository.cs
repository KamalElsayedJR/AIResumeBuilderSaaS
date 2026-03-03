using AIResumeBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Interfaces.Repositories
{
    public interface IExperienceRepository
    {
        public Task<Experience?> GetExperienceByIdForSpecificResumeAsync(int ExperienceId, int ResumeId, int UserId);
    }
}
