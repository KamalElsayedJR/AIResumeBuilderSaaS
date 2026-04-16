using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Interfaces.Services;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIResumeBuilder.Infrastructure.Implementation.Services
{
    internal class StripePaymentService : IPaymentService
    {
        public async Task<DataResponse<string>> CreateCheckoutSessionAsync(int userId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",

                SuccessUrl = "https://localhost:7043/api/Usage/getusage",
                CancelUrl = "https://localhost:7043/api/Usage/getusage",

                Metadata = new Dictionary<string, string>
                {
                    { "userId", userId.ToString() }
                },

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Quantity = 1,
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmount = 1000,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Pro Plan"
                            }
                        }
                    }
                }
            };
            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return new DataResponse<string>
            {
                Data = session.Url,
                Success = true,
                Message = "Checkout session created successfully."
            };
        }
    }
}
