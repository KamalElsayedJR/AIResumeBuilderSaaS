using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Experience;
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
    public class ResumeExperienceService : IResumeExperienceService
    {
        private readonly IUnitOfWork _uoW;
        private readonly IMapper _mapper;

        public ResumeExperienceService(IUnitOfWork UoW, IMapper mapper)
        {
            _uoW = UoW;
            _mapper = mapper;
        }
        public async Task<BaseResponse> AddExperienceAsync(AddExperienceDto dto, int ResumeId, int UserId)
        {
            var resume = await _uoW.ResumeRepository.GetByIdAsync(ResumeId, UserId);
            if (resume is null)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Resume not found",
                };
            }
            var now = DateTime.UtcNow;
            var minDate = now.AddYears(-20);

            #region StartDate Validation
            // StartDate must not be in the future
            if (dto.StartDate > now)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Start date cannot be in the future."
                };
            }

            // StartDate must not be more than 20 years ago
            if (dto.StartDate < minDate)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Start date cannot be more than 20 years ago."
                };
            }
            #endregion
            #region EndDate Validation
            if (dto.IsCurrent)
            {
                if (dto.EndDate.HasValue)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Current experience cannot have an end date."
                    };
                }
            }
            else
            {
                if (!dto.EndDate.HasValue)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "End date is required when experience is not current."
                    };
                }

                if (dto.EndDate <= dto.StartDate)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "End date must be after start date."
                    };
                }

                if (dto.EndDate > now)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "End date cannot be in the future."
                    };
                }

                if (dto.EndDate < minDate)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "End date cannot be more than 20 years ago."
                    };
                }
            }
            #endregion
            var experience = _mapper.Map<AddExperienceDto, Experience>(dto);
            experience.ResumeId = ResumeId;
            await _uoW.Repository<Experience>().AddAsync(experience);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                return new BaseResponse
                {
                    Success = true,
                    Message = "Experience added successfully",
                };
            }
            return new BaseResponse
            {
                Success = false,
                Message = "Failed to add experience",
            };
        }

        public Task<DataResponse<ExperienceDto>> UpdateExperienceAsync(UpdateExperienceDto dto, int ResumeId, int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
