using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace UseCase2_NET_Senior_Dima_Panteliuk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly BalanceService _balanceService;

        public StripeController(BalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet]
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
