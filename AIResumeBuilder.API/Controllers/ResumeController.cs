using AIResumeBuilder.API.Dtos;
using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.Resume;
using AIResumeBuilder.Application.Services.Interfaces;
using AIResumeBuilder.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIResumeBuilder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;
        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }
        [HttpPost]
        public async Task<ActionResult<DataResponse<ResumeDto>>> CreateResume(CreateResumeRequest request)
        {

            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int s);
            var response = await _resumeService.CreateResumeAsync(request.Title, request.Summary, s);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResumeDto>>>> GetResumes()
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int UserId);
            var response = await _resumeService.GetMyResumesAsync(UserId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("{ResumeId}")]
        public async Task<ActionResult<DataResponse<ResumeDto>>> GetResumeById([FromRoute] int ResumeId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int s);
            var response = await _resumeService.GetResumeByIdAsync(ResumeId, s);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpDelete("{ResumeId}")]
        public async Task<ActionResult<DataResponse<string>>> DeleteResume([FromRoute] int ResumeId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int s);
            var response = await _resumeService.DeleteResumeAsync(ResumeId, s);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut("{ResumeId}")]
        public async Task<ActionResult<DataResponse<ResumeDto>>> UpdateResume(UpdateResumeDto request, [FromRoute] int ResumeId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int s);
            var response = await _resumeService.UpdateResumeAsync(request, ResumeId, s);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }
        [AllowAnonymous]
        [HttpGet("public/{slug}")]
        public async Task<ActionResult<DataResponse<ResumeDto>>> GetResumeBySlug([FromRoute] string slug)
        {
            var response = await _resumeService.GetResumeBySlugAsync(slug);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
