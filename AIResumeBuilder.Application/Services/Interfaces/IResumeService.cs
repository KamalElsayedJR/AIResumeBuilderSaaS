using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Services.Interfaces
{
    public interface IResumeService
    {
        public Task<DataResponse<ResumeDto>> CreateResumeAsync(string Title,string Summary, int UserId);
        public Task<DataResponse<List<ResumeDto>>> GetMyResumesAsync(int UserId);
        public Task<DataResponse<ResumeDto>> GetResumeByIdAsync(int ResumeId, int UserId);
        public Task<DataResponse<ResumeDto>> UpdateResumeAsync(UpdateResumeDto dto,int ResumeId,int UserId);
        public Task<BaseResponse> DeleteResumeAsync(int ResumeId,int UserId);

    }
}
