using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Experience;
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

        public Task<DataResponse<ExperienceDto>> UpdateExperienceAsync(UpdateExperienceDto dto, int ResumeId, int UserId);
        //public Task DeleteExperienceAsync(int experienceId, int ResumeId, int UserId);


    }
}
