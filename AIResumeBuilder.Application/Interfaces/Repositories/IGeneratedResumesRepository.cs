using AIResumeBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Interfaces.Repositories
{
    public interface IGeneratedResumesRepository
    {
        Task<GeneratedResumes?> GetGeneratedResumesAsync(int resumeId,int UserId);
    }
}
