using Microsoft.AspNetCore.Mvc;

namespace portfolio_backend;
[ApiController]
[Route("api/[controller]")]
public class PortfolioBackendController : ControllerBase
{
    private readonly ILogger<PortfolioBackendController> _logger;

    public PortfolioBackendController(ILogger<PortfolioBackendController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public TextPost Get()
    {
        _logger.LogInformation("It's working");
        return new (){
          Id = 1,
          Name = "Test",
          Text = "Hello World!"
        };
    }
}
