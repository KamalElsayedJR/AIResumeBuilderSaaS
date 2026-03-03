using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Experience;
using AIResumeBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Services.Interfaces
{
    public interface IResumeExperienceService
    {
        public Task<BaseResponse> AddExperienceAsync(AddExperienceDto dto,int ResumeId ,int UserId );

        public Task<DataResponse<ExperienceDto>> UpdateExperienceAsync(UpdateExperienceDto dto,int ExperienceId, int ResumeId, int UserId);
        public Task<BaseResponse> DeleteExperienceAsync(int experienceId, int ResumeId, int UserId);


    }
}
