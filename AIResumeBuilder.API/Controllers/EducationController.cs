using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Education;
using AIResumeBuilder.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIResumeBuilder.API.Controllers
{
    [Route("api/Resume")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IResumeEducationService _rEducationService;

        public EducationController(IResumeEducationService rEducationService)
        {
            _rEducationService = rEducationService;
        }
        [HttpPost("{ResumeId}/Education")]
        public async Task<ActionResult<BaseResponse>> AddNewEducation(AddEducationDto dto, int ResumeId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int UserId);
            var response = await _rEducationService.AddEducationAsync(dto, ResumeId, UserId);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpDelete("{ResumeId}/Education/{EducationId}")]
        public async Task<ActionResult<BaseResponse>> DeleteEducation(int EducationId, int ResumeId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int UserId);
            var response = await _rEducationService.DeleteEducationAsync(EducationId, ResumeId, UserId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut("{ResumeId}/Education/{EducationId}")]
        public async Task<ActionResult<BaseResponse>> UpdateEducation(UpdateEducationDto dto, int EducationId, int ResumeId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int UserId);
            var response = await _rEducationService.UpdateEducationAsync(dto,EducationId, ResumeId, UserId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
