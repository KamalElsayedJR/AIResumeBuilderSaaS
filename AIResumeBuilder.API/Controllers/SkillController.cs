using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume.Skill;
using AIResumeBuilder.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIResumeBuilder.API.Controllers
{
    [Route("api/Resume")]
    [ApiController]
    [Authorize]
    public class SkillController : ControllerBase
    {
        private readonly IResumeSkillService _rSkillService;

        public SkillController(IResumeSkillService resumeSkillService)
        {
            _rSkillService = resumeSkillService;
        }
        [HttpPost("{ResumeId}/skill")]
        public async Task<ActionResult<BaseResponse>> AddSkill(AddSkillDto dto, [FromRoute] int ResumeId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int UserId);
            var response = await _rSkillService.AddNewSkillAsync(dto, ResumeId, UserId);
            if (response.Success)
            {
                return Ok(response);
            }
            ;
            return BadRequest(response);
        }
        [HttpPut("{ResumeId}/skill/{SkillId}")]
        public async Task<ActionResult<DataResponse<SkillDto>>> UpdateSkill(UpdateSkillDto dto, int ResumeId, int SkillId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int UserId);
            var respone = await _rSkillService.UpdateSkillAsync(dto, ResumeId, SkillId, UserId);
            if (respone.Success)
            {
                return Ok(respone);
            }
            return BadRequest(respone);
        }
        [HttpDelete("{ResumeId}/skill/{SkillId}")]
        public async Task<ActionResult<BaseResponse>> DeleteSkill(int ResumeId, int SkillId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int UserId);
            var response = await _rSkillService.DeleteSkillAsync(SkillId, ResumeId, UserId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }
    }
}
