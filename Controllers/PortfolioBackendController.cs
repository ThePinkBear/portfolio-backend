using Microsoft.AspNetCore.Mvc;

namespace portfolio_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PortfolioBackendController : ControllerBase
{
    private readonly ILogger<PortfolioBackendController> _logger;

    public PortfolioBackendController(ILogger<PortfolioBackendController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public object Get()
    {
        _logger.LogInformation("It's working");
        return new {
          id = 1,
          text = "Hello World!"
        };
    }
}