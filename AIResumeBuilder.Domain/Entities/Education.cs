using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Domain.Entities
{
    public class Education : BaseEntity
    {
        public string InstitutionName { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string? Grade { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
