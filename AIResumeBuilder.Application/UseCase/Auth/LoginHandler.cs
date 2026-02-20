using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Auth;
using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Application.Interfaces.Services;
using AIResumeBuilder.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.UseCase.Auth
{
    public class LoginHandler
    {
        private readonly IUnitOfWork _uoW;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public LoginHandler(IUnitOfWork UoW,IPasswordService passwordService,ITokenService tokenService,IMapper mapper) 
        {
            _uoW = UoW;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<DataResponse<LoginResponse>> LoginAsync(string Email,string Password)
        {
            var user = await _uoW.UserRepository.GetUserByEmailAsync(Email);
            if (user is null)
            {
                return new DataResponse<LoginResponse>()
                {
                    Success = false,
                    Message = "Credintional not correct",
                };
            }
            if (!_passwordService.VerifyPassword(Password,user.HashedPassword))
            {
                return new DataResponse<LoginResponse>()
                {
                    Success = false,
                    Message = "Credintional not correct",
                };
            }
            var data = _mapper.Map<User,LoginResponse>(user);
            data.AccessToken = _tokenService.GenerateAccessToken(user);
            data.RefreshToken = _tokenService.GenerateRefreshToken();
            return new DataResponse<LoginResponse>()
            {
                Success = true,
                Message = "Login successfully",
                Data = data
            };
        }
    }
}
