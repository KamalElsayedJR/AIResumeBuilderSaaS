using AIResumeBuilder.Application.Dtos.AI;
using AIResumeBuilder.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Services.Implementation
{
    public class ResumeRenderService : IResumeRenderService
    {
        public string GenerateHtml(AiResponse data)
        {
            var skillsHtml = string.Join("", data.Skills.Select(s => $"<li>{s}</li>"));
            var experienceHtml = string.Join("", data.Experience.Select(e => $"<li>{e}</li>"));
            var educationHtml = string.Join("", data.Education.Select(e => $"<li>{e}</li>"));
            var html = $@"
                            <!DOCTYPE html>
                            <html>
                            <head>
                                <meta charset='UTF-8'>
                                <style>
                                    body {{
                                        font-family: Arial, sans-serif;
                                        margin: 40px;
                                        color: #333;
                                    }}

                                    h1 {{
                                        text-align: center;
                                        margin-bottom: 5px;
                                    }}

                                    h2 {{
                                        border-bottom: 2px solid #eee;
                                        padding-bottom: 5px;
                                        margin-top: 25px;
                                    }}

                                    p {{
                                        line-height: 1.6;
                                    }}

                                    ul {{
                                        padding-left: 20px;
                                    }}

                                    li {{
                                        margin-bottom: 6px;
                                    }}
                                </style>
                            </head>

                            <body>

                                <h1>Professional Resume</h1>

                                <h2>Summary</h2>
                                <p>{data.Summary}</p>

                                <h2>Skills</h2>
                                <ul>
                                    {skillsHtml}
                                </ul>

                                <h2>Experience</h2>
                                <ul>
                                    {experienceHtml}
                                </ul>

                                <h2>Education</h2>
                                <ul>
                                    {educationHtml}
                                </ul>

                            </body>
                            </html>
                            ";
            return html;
        }
    }
}
