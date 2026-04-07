using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Dtos.AI
{
    public class AiResponse : BaseResponse
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("skills")]
        public List<string> Skills { get; set; }

        [JsonPropertyName("experience")]
        public List<string> Experience { get; set; }

        [JsonPropertyName("education")]
        public List<string> Education { get; set; }
    }
    public class GeminiResponse
    {
        public List<Candidate> Candidates { get; set; }
    }
    public class Candidate
    {
        public Content Content { get; set; }
    }

    public class Content
    {
        public List<Part> Parts { get; set; }
    }

    public class Part
    {
        public string Text { get; set; }
    }
}
