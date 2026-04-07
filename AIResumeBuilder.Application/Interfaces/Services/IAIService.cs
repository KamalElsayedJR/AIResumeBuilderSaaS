using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.AI;
using AIResumeBuilder.Application.Dtos.Resume;
using AIResumeBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Interfaces.Services
{
    public interface IAIService
    {
        Task<DataResponse<AiResponse>> GenerateFullResume(int resumeId,int UserId);
    }
}
