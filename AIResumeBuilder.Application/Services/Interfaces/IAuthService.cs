using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<DataResponse<LoginResponse>> LoginAsync(string Email, string Password);
        public Task<BaseResponse> RegisterAsync(RegisterDto dto);

    }
}
