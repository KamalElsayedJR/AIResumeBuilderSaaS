using AIResumeBuilder.API.Dtos;
using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Auth;
using AIResumeBuilder.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIResumeBuilder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse>> RegisterAsync(RegisterDto request)
        {
            var response =await _authService.RegisterAsync(request);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<DataResponse<LoginResponse>>> LoginAsync(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request.Email, request.Password);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized(response);
            }
        }
        [HttpGet("Me")]
        [Authorize]
        public ActionResult Profile()
        {
            return Ok("TEst");
        }
    }
}
