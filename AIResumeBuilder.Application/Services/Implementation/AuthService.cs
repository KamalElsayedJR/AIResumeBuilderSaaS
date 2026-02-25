using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Auth;
using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Application.Interfaces.Services;
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
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uoW;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AuthService(IUnitOfWork UoW, IPasswordService passwordService, ITokenService tokenService, IMapper mapper)
        {
            _uoW = UoW;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<DataResponse<LoginResponse>> LoginAsync(string Email, string Password)
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
            if (!_passwordService.VerifyPassword(Password, user.HashedPassword))
            {
                return new DataResponse<LoginResponse>()
                {
                    Success = false,
                    Message = "Credintional not correct",
                };
            }
            var data = _mapper.Map<User, LoginResponse>(user);
            data.AccessToken = _tokenService.GenerateAccessToken(user);
            data.RefreshToken = _tokenService.GenerateRefreshToken();
            return new DataResponse<LoginResponse>()
            {
                Success = true,
                Message = "Login successfully",
                Data = data
            };
        }
        public async Task<BaseResponse> RegisterAsync(RegisterDto dto)
        {
            if (string.IsNullOrEmpty(dto.FullName) || string.IsNullOrEmpty(dto.Email)
                || dto.Age == null || string.IsNullOrEmpty(dto.Password)
                || string.IsNullOrEmpty(dto.PhoneNumber))
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Requierd Field Is Empty"
                };

            }
            var userExist = await _uoW.UserRepository.GetUserByEmailAsync(dto.Email.Trim().ToLower());
            if (userExist != null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Email Already Exist"
                };
            }
            if (dto.Password != dto.ConfirmPassword)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Password And Confirm Password Not Match"
                };
            }
            if (dto.Age < 18)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "You Must Be At Least 18 Years Old To Register"
                };
            }
            if (dto.Password.Length < 8)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Password Must Be Less Than 8 Characters"
                };
            }
            var user = new User()
            {
                FullName = dto.FullName.Trim(),
                Email = dto.Email.Trim(),
                HashedPassword = _passwordService.HashPassword(dto.Password.Trim()),
                Age = dto.Age,
                PhoneNumber = dto.PhoneNumber.Trim(),
            };
            await _uoW.Repository<User>().AddAsync(user);
            var result = await _uoW.SaveChangesAsync();
            if (result <= 0)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Faild To Create User"
                };
            }
            return new BaseResponse()
            {
                Success = true,
                Message = "User Created Successfully"
            };
        }
    }
}
