using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Services.Interfaces
{
    public interface IResumeSkillService
    {
        public Task<BaseResponse> AddNewSkillAsync(AddSkillDto dto,int ResumeId,int UserId);
        public Task<DataResponse<SkillDto>> UpdateSkillAsync(UpdateSkillDto dto ,int SkillId,int ResmumeId,int UserId);
        public Task<BaseResponse> DeleteSkillAsync(int SkillId,int ResumeId,int UserId);

    }
}

