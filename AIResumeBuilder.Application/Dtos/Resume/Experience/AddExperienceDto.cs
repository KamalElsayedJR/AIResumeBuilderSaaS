using AIResumeBuilder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Dtos.Resume.Experience
{
    public class AddExperienceDto
    {
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string? Location { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}
