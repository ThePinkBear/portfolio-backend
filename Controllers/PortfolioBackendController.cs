using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace portfolio_backend;
[ApiController]
[Route("api/[controller]")]
public class PortfolioBackendController : ControllerBase
{
  public string PUBLIC = "You have permission to view this public post.";
  public string PRIVATE = "You have permission to view this private post.";
  public string ADMIN = "You have admin permission to view this post.";

  [HttpGet("public")]
  public IActionResult Public() 
    =>Ok(new { message = PUBLIC });

  [HttpGet("private")]
  [Authorize]
  public IActionResult Private() 
    => Ok(new { message = PRIVATE });

  [HttpGet("read-scoped")]
  [Authorize("admin:edit")]
  public IActionResult ReadScoped()
    => Ok(new { message = ADMIN });
  
  [HttpGet("claims")]
  public IActionResult Claims()
    => Ok(User.Claims.Select(c => new { c.Type, c.Value }));
}
