using AIResumeBuilder.Domain.Entities;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> ExistsByStripeEventIdAsync(string stripeEventId);
        Task AddAsync(Payment payment);
    }
}
