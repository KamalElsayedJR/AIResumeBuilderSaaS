using AIResumeBuilder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AIResumeBuilder.Domain.Entities
{
    public class Experience : BaseEntity
    {
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string? Location { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
        
    }
}
