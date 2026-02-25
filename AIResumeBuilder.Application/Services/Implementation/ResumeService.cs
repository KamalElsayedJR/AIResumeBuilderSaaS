using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume;
using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Application.Services.Interfaces;
using AIResumeBuilder.Domain.Entities;
using AIResumeBuilder.Domain.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AIResumeBuilder.Application.Services.Implementation
{
    public class ResumeService : IResumeService
    {
        private readonly IUnitOfWork _uoW;
        private readonly IMapper _mapper;

        public ResumeService(IUnitOfWork UoW, IMapper mapper)
        {
            _uoW = UoW;
            _mapper = mapper;
        }
        public async Task<DataResponse<ResumeDto>> CreateResumeAsync(string Title, string Summary, int UserId)
        {
            var user = await _uoW.Repository<User>().GetByIdAsync(UserId);
            var userresume = await _uoW.ResumeRepository.GetByUser(UserId);
            if (user.Plan == Plan.Free && userresume.Count() > 0)
            {
                return new DataResponse<ResumeDto>()
                {
                    Success = false,
                    Message = "Free plan users can only create one resume. Please upgrade to Pro plan to create more resumes."
                };
            }
            var resume = new Resume()
            {
                Title = Title,
                Summray = Summary,
                UserId = UserId,
            };
            await _uoW.Repository<Resume>().AddAsync(resume);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                return new DataResponse<ResumeDto>()
                {
                    Success = true,
                    Message = "Resume created successfully",
                    Data = _mapper.Map<ResumeDto>(resume)
                };
            }
            return new DataResponse<ResumeDto>()
            {
                Success = result > 0,
                Message = "Failed to create resume",
            };
        }
        public async Task<BaseResponse> DeleteResumeAsync(int ResumeId, int UserId)
        {
            var resume = await _uoW.ResumeRepository.GetByIdAsync(ResumeId, UserId);
            if (resume is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Resume not found",
                };
            }
            resume.IsDeleted = true;
            _uoW.Repository<Resume>().Update(resume);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                return new BaseResponse()
                {
                    Success = true,
                    Message = "Resume deleted successfully",
                };
            }
            return new BaseResponse()
            {
                Success = false,
                Message = "Failed to delete resume",
            };
        }
        public async Task<DataResponse<List<ResumeDto>>> GetMyResumesAsync(int UserId)
        {
            var resumes = (await _uoW.ResumeRepository.GetByUser(UserId)).Where(r=>r.IsDeleted = false);
            if (resumes is null)
            {
                return new DataResponse<List<ResumeDto>>()
                {
                    Success = false,
                    Message = "No resumes found for the user",
                    Data = null
                };
            }
            return new DataResponse<List<ResumeDto>>()
            {
                Success = true,
                Message = "Resumes retrieved successfully",
                Data = _mapper.Map<List<ResumeDto>>(resumes)
            };
        }
        public async Task<DataResponse<ResumeDto>> GetResumeByIdAsync(int ResumeId, int UserId)
        {
            var resume = (await _uoW.ResumeRepository.GetByIdAsync(ResumeId, UserId));
            if (resume is null)
            {
                return new DataResponse<ResumeDto>()
                {
                    Success = false,
                    Message = "Resume not found",
                };
            }
            return new DataResponse<ResumeDto>()
            {
                Success = true,
                Message = "Resume retrieved successfully",
                Data = _mapper.Map<ResumeDto>(resume)
            };
        }
        public async Task<DataResponse<ResumeDto>> UpdateResumeAsync(UpdateResumeDto dto,int ResumeId,int UserId)
        {
            var resume = await _uoW.ResumeRepository.GetByIdAsync(ResumeId, UserId);
            if (resume is null)
            {
                return new DataResponse<ResumeDto>()
                {
                    Success = false,
                    Message = "Resume not found",
                };
            }
            if (!string.IsNullOrEmpty(dto.Title))
            {
                if (dto.Title != resume.Title)
                {
                    resume.Title = dto.Title;
                }
            }
            else
            {
                return new DataResponse<ResumeDto>()
                {
                    Success = false,
                    Message = "Title Can Not Be Empty",
                };
            }
            if (!string.IsNullOrEmpty(dto.Summray))
            {
                if (dto.Summray != resume.Summray)
                {
                    resume.Summray = dto.Summray;
                }
            }
            else
            {
                return new DataResponse<ResumeDto>()
                {
                    Success = false,
                    Message = "Summary Can Not Be Empty",
                };
            }
            resume.UpdatedAt = DateTime.UtcNow;
            _uoW.Repository<Resume>().Update(resume);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                return new DataResponse<ResumeDto>()
                {
                    Success = true,
                    Message = "Resume updated successfully",
                    Data = _mapper.Map<ResumeDto>(resume)
                };
            }
            return new DataResponse<ResumeDto>()
            {
                Success = false,
                Message = "resume is not updated",
            };
        }

    }
}
