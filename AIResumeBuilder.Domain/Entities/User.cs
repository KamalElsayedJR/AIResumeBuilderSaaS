using AIResumeBuilder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public Plan Plan { get; set; } = Plan.Free;
        public SubscriptionStatus SubscriptionStatus { get; set; } = SubscriptionStatus.Active;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public ICollection<Resume> Resumes { get; set; } = new List<Resume>();
    }
}
