using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.AI;
using AIResumeBuilder.Application.Interfaces.Services;
using AIResumeBuilder.Application.Services.Interfaces;
using AIResumeBuilder.Domain.Entities;
using AIResumeBuilder.Infrastructure.Implementation.Services;
using Azure;
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
        private readonly IResumeRenderService _resumeRenderService;
        private readonly IPdfService _pdfService;

        public AiController(IAIService aIService, IResumeRenderService resumeRenderService, IPdfService pdfService)
        {
            _aIService = aIService;
            _resumeRenderService = resumeRenderService;
            _pdfService = pdfService;
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
        [HttpPost("{id}/generate-pdf")]
        public async Task<ActionResult> GenerateResumepdf(int id)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var result = await _aIService.GenerateFullResume(id, userId);
            if (result is null || !result.Success)
            {
                return BadRequest(result);
            }
            var html = _resumeRenderService.GenerateHtml(result.Data);

            var pdfBytes = _pdfService.GeneratePdfFromHtml(html);
            return File(pdfBytes, "application/pdf", $"resume_{DateTime.Now:yyyyMMddHHmm}.pdf");
        }
        [HttpPost("{id}/generate-html")]
        public async Task<ActionResult> GenerateResumeHtml(int id)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var result = await _aIService.GenerateFullResume(id, userId);
            if (!result.Success || result is null)
            {
                return BadRequest(result);
            }
            var html = _resumeRenderService.GenerateHtml(result.Data);

            return Content(html, "text/html");
        }

        [HttpPost("{id}/regenerate")]
        public async Task<ActionResult<DataResponse<AiResponse>>> ReGenerateResume(int id)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var response = await _aIService.ReGenerateResume(id, userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
