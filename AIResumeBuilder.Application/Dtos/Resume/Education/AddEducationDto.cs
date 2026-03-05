using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Dtos.Resume.Education
{
    public class AddEducationDto
    {
        [Required]

        public string InstitutionName { get; set; }
        [Required]

        public string Degree { get; set; }
        [Required]

        public string FieldOfStudy { get; set; }
        public string? Grade { get; set; }
        public string? Description { get; set; }
        [Required]

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
    }
}
