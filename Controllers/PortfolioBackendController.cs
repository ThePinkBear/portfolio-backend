using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace portfolio_backend;
[ApiController]
[Route("api/[controller]")]
public class PortfolioBackendController : ControllerBase
{
  private readonly IConfiguration _config;
  public PortfolioBackendController(IConfiguration config)
  {
    _config = config;
  }

  [HttpGet("public")]
  public IActionResult Public() 
    =>Ok(new { message = _config.GetSection("Messages:public").Value });

  [HttpGet("profile-pictures")]
  public IActionResult ProfilePictures(string image) 
    => Ok($"{_config.GetSection("Images:url").Value}{image}?token={_config.GetSection("Images:token").Value}");
  

  [HttpGet("private")]
  [Authorize]
  public IActionResult Private() 
    => Ok(new { message = _config.GetSection("Messages:private").Value });

  [HttpGet("read-scoped")]
  [Authorize("admin:edit")]
  public IActionResult ReadScoped()
    => Ok(new { message = _config.GetSection("Messages:admin").Value });
  
  [HttpGet("claims")]
  public IActionResult Claims()
    => Ok(User.Claims.Select(c => new { c.Type, c.Value }));
}
