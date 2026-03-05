using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Education;
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
    public class ResumeEducationService : IResumeEducationService
    {
        private readonly IUnitOfWork _uoW;
        private readonly IMapper _mapper;

        public ResumeEducationService(IUnitOfWork UoW, IMapper mapper)
        {
            _uoW = UoW;
            _mapper = mapper;
        }
        public async Task<BaseResponse> AddEducationAsync(AddEducationDto dto, int ResumeId, int UserId)
        {
            var resume = await _uoW.ResumeRepository.GetByIdAsync(ResumeId, UserId);
            if (resume is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Resume not found."
                };
            }
            var education = _mapper.Map<AddEducationDto, Education>(dto);
            education.ResumeId = ResumeId;
            await _uoW.Repository<Education>().AddAsync(education);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                return new BaseResponse()
                {
                    Success = true,
                    Message = "Education added successfully."
                };
            }
            return new BaseResponse()
            {
                Success = false,
                Message = "Failed to add education."
            };
        }

        public async Task<BaseResponse> DeleteEducationAsync(int EducationId, int ResumeId, int UserId)
        {
            var education  = await _uoW.EducationRepository.GetEducationByIdForResumeAndUser(EducationId,ResumeId,UserId);
            if (education is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Education not found."
                };
            }
            _uoW.Repository<Education>().Delete(education);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                return new BaseResponse()
                {
                    Success = true,
                    Message = "Education deleted successfully."
                };
            }
            return new BaseResponse()
            {
                Success = false,
                Message = "Failed to delete education."
            };
        }

        public async Task<DataResponse<EducationDto>> UpdateEducationAsync(UpdateEducationDto dto, int EductionId, int ResumeId, int UserId)
        {
            var education = await _uoW.EducationRepository.GetEducationByIdForResumeAndUser(EductionId, ResumeId, UserId);
            if (education is null)
            {
                return new DataResponse<EducationDto>()
                {
                    Success = false,
                    Message = "Resume not found."
                };
            }
            education = _mapper.Map(dto, education);
            education.UpdatedAt = DateTime.UtcNow;
            _uoW.Repository<Education>().Update(education);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                var educationDto = _mapper.Map<Education, EducationDto>(education);
                return new DataResponse<EducationDto>()
                {
                    Success = true,
                    Message = "Education updated successfully.",
                    Data = educationDto
                };
            }
            return new DataResponse<EducationDto>()
            {
                Success = false,
                Message = "Failed to update education."

            };
        }

    }
}
