using AIResumeBuilder.Application.Dtos;
using AIResumeBuilder.Application.Interfaces.Repositories;
using AIResumeBuilder.Application.Interfaces.Services;
using AIResumeBuilder.Domain.Entities;
using AIResumeBuilder.Domain.Enums;
using AIResumeBuilder.Infrastructure.Implementation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace AIResumeBuilder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _uoW;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, IConfiguration configuration, IUnitOfWork UoW, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _configuration = configuration;
            _uoW = UoW;
            _logger = logger;
        }
        [Authorize]

        [HttpPost("upgrade")]
        public async Task<ActionResult<DataResponse<string>>> Upgrade()
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var response = await _paymentService.CreateCheckoutSessionAsync(userId);

            return Ok(new DataResponse<string>
            {
                Data = response.Data,
                Success = response.Success,
                Message = response.Message
            });
        }

        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                                    Request.Headers["Stripe-Signature"],
                                    _configuration["Stripe:WebhookSecret"],
                                    throwOnApiVersionMismatch: false);

                _logger.LogInformation("Stripe webhook received. EventId: {EventId}, Type: {Type}", stripeEvent.Id, stripeEvent.Type);

                if (string.IsNullOrWhiteSpace(stripeEvent.Id))
                {
                    _logger.LogWarning("Stripe webhook event id is missing.");
                    return Ok();
                }

                if (await _uoW.PaymentRepository.ExistsByStripeEventIdAsync(stripeEvent.Id))
                {
                    _logger.LogInformation("Duplicate Stripe webhook detected and ignored. EventId: {EventId}", stripeEvent.Id);
                    return Ok();
                }

                if (stripeEvent.Type != "checkout.session.completed")
                    return Ok();

                if (stripeEvent.Data.Object is not Session session)
                {
                    _logger.LogWarning("Stripe webhook data object is not a checkout session. EventId: {EventId}", stripeEvent.Id);
                    return Ok();
                }

                if (!string.Equals(session.PaymentStatus, "paid", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Checkout session is not paid. SessionId: {SessionId}, PaymentStatus: {PaymentStatus}", session.Id, session.PaymentStatus);
                    return Ok();
                }

                if (session.Metadata == null || !session.Metadata.TryGetValue("userId", out var userIdStr))
                {
                    _logger.LogWarning("Missing userId metadata in checkout session. SessionId: {SessionId}", session.Id);
                    return Ok();
                }

                if (!int.TryParse(userIdStr, out var userId))
                {
                    _logger.LogWarning("Invalid userId metadata in checkout session. SessionId: {SessionId}, userId: {UserId}", session.Id, userIdStr);
                    return Ok();
                }

                var user = await _uoW.Repository<User>().GetByIdAsync(userId);

                if (user == null)
                {
                    _logger.LogWarning("User not found while processing checkout session. UserId: {UserId}, SessionId: {SessionId}", userId, session.Id);
                    return Ok();
                }

                user.Plan = Domain.Enums.Plan.Pro;
                user.SubscriptionEndDate = DateTime.UtcNow.AddMonths(1);
                _uoW.Repository<User>().Update(user);

                var amount = (session.AmountTotal ?? 0) / 100m;
                var payment = new Payment
                {
                    StripeEventId = stripeEvent.Id,
                    StripeSessionId = session.Id ?? string.Empty,
                    UserId = userId,
                    Amount = amount,
                    Currency = string.IsNullOrWhiteSpace(session.Currency) ? "usd" : session.Currency,
                    Status = session.PaymentStatus,
                    CreatedAt = DateTime.UtcNow
                };

                await _uoW.PaymentRepository.AddAsync(payment);
                await _uoW.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing Stripe webhook.");
                return BadRequest();
            }
        }
    }
}
