using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Domain.Entities;
using AIResumeBuilder.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIResumeBuilder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsageController : ControllerBase
    {
        private readonly IUnitOfWork _uoW;

        public UsageController(IUnitOfWork UoW)
        {
            _uoW = UoW;
        }
        [HttpGet("getusage")]
        public async Task<ActionResult<BaseResponse>> GetUsage()
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var count = await _uoW.GeneratedResumesRepository.CountThisMonthAsync(userId);
            var user = await _uoW.Repository<User>().GetByIdAsync(userId);
            int limit = user.Plan == Plan.Free ? 2 : 25;
            int remaining = Math.Max(0, limit - count);
            return Ok(new BaseResponse
                {
                    Success = true,
                    Message = $"You have generated {count} resumes this month. You can generate {remaining} more resumes before reaching your limit of {limit} resumes per month."
                });
        }
    }
}