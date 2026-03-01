using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Experience;
using AIResumeBuilder.Application.Services.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIResumeBuilder.API.Controllers
{
    [Authorize]
    [Route("api/Resume")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IResumeExperienceService _rExperienceService;

        public ExperienceController(IResumeExperienceService resumeExperienceService)
        {
            _rExperienceService = resumeExperienceService;
        }
        [HttpPost("{ResumeId}/experience")]
        public async Task<ActionResult<BaseResponse>> AddExperience(AddExperienceDto requset, [FromRoute] int ResumeId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userid);
            var response = await _rExperienceService.AddExperienceAsync(requset, ResumeId, userid);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
