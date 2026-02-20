using AIResumeBuilder.API.Dtos;
using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Auth;
using AIResumeBuilder.Application.UseCase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIResumeBuilder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RegisterHandler _registerHandler;
        private readonly LoginHandler _loginHandler;

        public AuthController(RegisterHandler registerHandler,LoginHandler loginHandler)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
        }
        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse>> RegisterAsync(RegisterDto request)
        {
            var response =await _registerHandler.RegisterAsync(request);
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
            var response = await _loginHandler.LoginAsync(request.Email, request.Password);
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
