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

        /// <summary>
        /// Get user's balance
        /// </summary>
        /// <returns>Balance</returns>
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

        /// <summary>
        /// Get transactions with pagination.
        /// </summary>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="lastItemKey">The key of the last item on the previous page.</param>
        /// <returns>List of transactions.</returns>
        [HttpGet("transactions")]
        public async Task<IActionResult> GetBalanceTransactionsAsync(int? pageSize, string? lastItemKey)
        {
            try
            {
                var options = new BalanceTransactionListOptions
                {
                    Limit = pageSize,
                    StartingAfter = lastItemKey
                };

                var transactions = await _balanceTransactionService.ListAsync(options);
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
