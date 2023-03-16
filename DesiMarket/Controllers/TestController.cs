using log4net.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DesiMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        public TestController(ILogger<TestController> logger) { _logger = logger; }
        public IActionResult Index()
        {
            _logger.LogDebug("Debugging is done");
            _logger.LogInformation("Welcome to DesiMarket");
            _logger.LogError("Invalid credentials");

            return Ok();
        }
    }
}
