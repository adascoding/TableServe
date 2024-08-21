using Microsoft.AspNetCore.Mvc;
using TableServe.API.Services;

namespace TableServe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaitersController : ControllerBase
    {
        private readonly IWaiterService _waiterService;
        private readonly ILogger<WaitersController> _logger;

        public WaitersController(IWaiterService waiterService, ILogger<WaitersController> logger)
        {
            _waiterService = waiterService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetWaiters()
        {
            try
            {
                var waiters = await _waiterService.GetWaitersAsync();
                return Ok(waiters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the list of waiters.");
                return StatusCode(500, "An unexpected error occurred while retrieving waiters.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWaiter([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name is required.");
            }

            try
            {
                var apiKey = await _waiterService.GenerateApiKeyAsync(name);
                return Ok(new { ApiKey = apiKey });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Attempted to create a waiter with an existing name: {Name}", name);
                return Conflict("An API key already exists for a waiter with this name.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a waiter with name: {Name}", name);
                return StatusCode(500, "An unexpected error occurred while creating the waiter.");
            }
        }
    }
}
