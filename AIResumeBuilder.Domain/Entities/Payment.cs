using System;

namespace AIResumeBuilder.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string StripeEventId { get; set; }
        public string StripeSessionId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
