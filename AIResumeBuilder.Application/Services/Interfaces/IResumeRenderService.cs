using AIResumeBuilder.Application.Dtos.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Services.Interfaces
{
    public interface IResumeRenderService
    {
        string GenerateHtml(AiResponse data);
    }

}
