using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace portfolio_backend;
[ApiController]
[Route("api/[controller]")]
public class PortfolioBackendController : ControllerBase
{
    private readonly PortfolioDbContext _context;

    public PortfolioBackendController(PortfolioDbContext context)
    {
      _context = context;
    }

   [HttpGet]
    public async Task<ActionResult<IEnumerable<TextPost>>> GetTextPost()
    {
      return await _context.TextPost.ToListAsync();
    }
    [HttpGet("test")]
    public string Test()
    {
      return "Hello World!";
    }
}
