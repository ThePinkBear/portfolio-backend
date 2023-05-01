using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace portfolio_backend;
[ApiController]
[Route("api/[controller]")]
public class PortfolioBackendController : ControllerBase
{
  public string PUBLIC = "You have permission to view this public post.";
  public string PRIVATE = "You have permission to view this private post.";
  public string ADMIN = "You have admin permission to view this post.";
  private readonly PortfolioDbContext _context;

  public PortfolioBackendController(PortfolioDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<IEnumerable<TextPost>>> GetTextPost()
  {
    return await _context.TextPost.ToListAsync();
  }

  [HttpGet("public")]
  public IActionResult Test()
  {
    return Ok(new { message = PUBLIC });
  }

  // [HttpGet("private")]
  // [Authorize]
  // public IActionResult Private()
  // {
  //   return Ok(new { message = PRIVATE });
  // }

  // [HttpGet("read-scoped")]
  // [Authorize("admin:edit")]
  // public IActionResult ReadScoped()
  // {
  //   return Ok(new { message = ADMIN });
  // }
  [HttpGet("claims")]
  public IActionResult Claims()
  {
    return Ok(
    User.Claims.Select(c => new { c.Type, c.Value })
    );
  }

}
