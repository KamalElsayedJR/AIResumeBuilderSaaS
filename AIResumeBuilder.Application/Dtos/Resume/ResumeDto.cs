using AIResumeBuilder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Dtos.Resume
{
    public class ResumeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summray { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
