using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Services.Interfaces
{
    public interface IResumeEducationService
    {
        public Task<BaseResponse> AddEducationAsync(AddEducationDto dto, int ResumeId, int UserId);
        public Task<DataResponse<EducationDto>> UpdateEducationAsync(UpdateEducationDto dto ,int EductionId,int ResumeId,int UserId);
        public Task<BaseResponse> DeleteEducationAsync(int EducationId, int ResumeId, int UserId);
    }
}
