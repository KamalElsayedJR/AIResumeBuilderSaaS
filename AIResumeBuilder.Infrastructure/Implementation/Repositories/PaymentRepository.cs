using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Domain.Entities;
using AIResumeBuilder.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Implementation.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _dbContext;

        public PaymentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByStripeEventIdAsync(string stripeEventId)
        {
            if (string.IsNullOrWhiteSpace(stripeEventId))
                return false;

            return await _dbContext.Set<Payment>().AnyAsync(p => p.StripeEventId == stripeEventId);
        }

        public async Task AddAsync(Payment payment)
        {
            await _dbContext.Set<Payment>().AddAsync(payment);
        }
    }
}
