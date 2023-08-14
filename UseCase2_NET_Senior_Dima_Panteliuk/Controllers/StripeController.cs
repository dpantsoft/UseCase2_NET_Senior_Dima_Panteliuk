using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.FinancialConnections;

namespace UseCase2_NET_Senior_Dima_Panteliuk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly BalanceService _balanceService;
        private readonly BalanceTransactionService _balanceTransactionService;

        public StripeController(BalanceService balanceService, BalanceTransactionService balanceTransactionService)
        {
            _balanceService = balanceService;
            _balanceTransactionService = balanceTransactionService;
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalanceAsync()
        {
            try
            {
                var balance = await _balanceService.GetAsync();
                return Ok(balance);
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe Exception: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetBalanceTransactionsAsync()
        {
            try
            {
                var transactions = await _balanceTransactionService.ListAsync();
                return Ok(transactions.Data);
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe Exception: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("testtransactions")]
        [Obsolete("For testing only")]
        public async Task<IActionResult> MakeTestTransactionAsync()
        {
            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = 100000,
                    Currency = "UAH",
                    Source = "tok_mastercard"
                };

                var service = new ChargeService();
                var charge = await service.CreateAsync(options);

                return Ok(charge);
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe Exception: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
