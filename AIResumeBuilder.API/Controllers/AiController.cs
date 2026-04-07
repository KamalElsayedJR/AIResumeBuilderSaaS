using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.AI;
using AIResumeBuilder.Application.Interfaces.Services;
using AIResumeBuilder.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AIResumeBuilder.API.Controllers
{
    [Route("api/[controller]/resumes")]
    [ApiController]
    [Authorize]
    public class AiController : ControllerBase
    {
        private readonly IAIService _aIService;

        public AiController(IAIService aIService)
        {
            _aIService = aIService;
        }
        [HttpPost("{id}/generate")]
        public async Task<ActionResult<DataResponse<AiResponse>>> GenerateResume(int id)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var response = await _aIService.GenerateFullResume(id, userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
