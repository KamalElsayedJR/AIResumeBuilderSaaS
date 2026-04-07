using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Dtos.AI;
using AIResumeBuilder.Application.Dtos.Resume;
using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Application.Interfaces.Services;
using AIResumeBuilder.Domain.Entities;
using AutoMapper;
using Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Implementation.Services
{
    public class AIService : IAIService
    {
        private readonly IUnitOfWork _uoW;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AIService(IUnitOfWork UoW, IMapper mapper, HttpClient httpClient, IConfiguration config)
        {
            _uoW = UoW;
            _mapper = mapper;
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<DataResponse<AiResponse>> GenerateFullResume(int resumeId, int UserId)
        {
            var resume = await _uoW.ResumeRepository.GetByIdAsync(resumeId, UserId);
            if (resume is null)
            {
                return new DataResponse<AiResponse>
                {
                    Success = false,
                    Message = "Resume not found",
                };
            }
            var maaped = _mapper.Map<Resume, ResumeDto>(resume);
            string prompt = $@"
                                Create a professional resume in JSON format.

                                Return ONLY valid JSON.

                                Use this exact format:

                                {{
                                  ""summary"": ""string"",
                                  ""skills"": [""string""],
                                  ""experience"": [""string""],
                                  ""education"": [""string""]
                                }}

                                Instructions:
                                - Use bullet points
                                - Make it ATS-friendly
                                - Use action verbs
                                - Keep it concise

                                Data:
                                Title: {maaped.Title}
                                Summary: {maaped.Summray}
                                Skills: {string.Join(", ", maaped.Skills)}
                                Experience: {string.Join("\n", maaped.Experiences)}
                                Education: {string.Join(", ", maaped.Educations)}
                                ";
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                },
            };
            var url = $"{_config["AIAPI:EndPoint"]}{_config["AIAPI:APIKey"]}";
            await Task.Delay(300); 
            var respone = await _httpClient.PostAsJsonAsync(url, requestBody);

            if (!respone.IsSuccessStatusCode)
            {
                var errorContent = await respone.Content.ReadAsStringAsync();
                return new DataResponse<AiResponse>
                {
                    Success = false,
                    Message = $"AI API call failed: {errorContent}",
                };
            }
            var result = await respone.Content.ReadFromJsonAsync<GeminiResponse>();
            var generatedResume = result?.Candidates?[0]?.Content?.Parts?[0]?.Text;
            if (string.IsNullOrEmpty(generatedResume))
            {
                return new DataResponse<AiResponse>
                {
                    Success = false,
                    Message = "AI API did not return a valid resume",
                };
            }
            generatedResume = generatedResume.Replace("```json", "")
                                                .Replace("```", "")
                                                .Trim();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var aiResume = JsonSerializer.Deserialize<AiResponse>(generatedResume, options);
            return new DataResponse<AiResponse>
            {
                Success = true,
                Message = "Resume generated successfully",
                Data = aiResume
            };
        }
    }
}
