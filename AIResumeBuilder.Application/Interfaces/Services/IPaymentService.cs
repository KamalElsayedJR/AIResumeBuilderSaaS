using AIResumeBuilder.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<DataResponse<string>> CreateCheckoutSessionAsync(int userId);
    }
}
