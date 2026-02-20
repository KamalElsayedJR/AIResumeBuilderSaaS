using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Auth;
using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Application.Interfaces.Services;
using AIResumeBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.UseCase.Auth
{
    public class RegisterHandler
    {
        private readonly IUnitOfWork _uoW;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public RegisterHandler(IUnitOfWork UoW, ITokenService tokenService, IPasswordService passwordService)
        {
            _uoW = UoW;
            _tokenService = tokenService;
            _passwordService = passwordService;
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
