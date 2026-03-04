using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Skill;
using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Application.Services.Interfaces;
using AIResumeBuilder.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AIResumeBuilder.Application.Services.Implementation
{
    public class ResumeSkillService : IResumeSkillService
    {
        private readonly IUnitOfWork _uoW;
        private readonly IMapper _mapper;

        public ResumeSkillService(IUnitOfWork UoW, IMapper mapper)
        {
            _uoW = UoW;
            _mapper = mapper;
        }
        public async Task<BaseResponse> AddNewSkillAsync(AddSkillDto dto, int ResumeId, int UserId)
        {
            var resume = await _uoW.ResumeRepository.GetByIdAsync(ResumeId, UserId);
            if (resume is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Resume Not Found"
                };
            }
            var skill = _mapper.Map<AddSkillDto, Skill>(dto);
            skill.ResumeId = resume.Id;
            await _uoW.Repository<Skill>().AddAsync(skill);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                return new BaseResponse()
                {
                    Success = true,
                    Message = "Skill Added Successfuly"
                };
            }
            return new BaseResponse()
            {
                Success = false,
                Message = "Skill Does Not Add"
            };
        }
        public async Task<BaseResponse> DeleteSkillAsync(int SkillId, int ResumeId, int UserId)
        {
            var skill = await _uoW.SkillRepositroy.GetSkillByIdForReaumAndUserAsync(SkillId, ResumeId, UserId);
            if (skill is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Skill Not Found"
                };
            }
            _uoW.Repository<Skill>().Delete(skill);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0) 
            { 
                return new BaseResponse()
                {
                    Success = true,
                    Message = "Skill Deleted Successfuly"
                };
            }
            return new BaseResponse()
            {
                Success = false,
                Message = "Skill Does Not Deleted"
            };
        }
        public async Task<DataResponse<SkillDto>> UpdateSkillAsync(UpdateSkillDto dto, int SkillId, int ResmumeId, int UserId)
        {
            var skill = await _uoW.SkillRepositroy.GetSkillByIdForReaumAndUserAsync(SkillId, ResmumeId, UserId);
            if (skill is null)
            {
                return new DataResponse<SkillDto>()
                {
                    Success = false,
                    Message = "Skill Not Found"
                };
            }
            _mapper.Map(dto, skill);
            skill.UpdatedAt = DateTime.UtcNow;
            _uoW.Repository<Skill>().Update(skill);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                return new DataResponse<SkillDto>()
                {
                    Success = true,
                    Message = "Skill Updated Successfuly",
                    Data = _mapper.Map<Skill, SkillDto>(skill)
                };
            }
            return new DataResponse<SkillDto>()
            {
                Success = false,
                Message = "Skill Does Not Updated"

            };
        }

    }
}
