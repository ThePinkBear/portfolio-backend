using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace portfolio_backend;
[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
  private readonly PortfolioDbContext _context;

  public ImagesController(PortfolioDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Image>>> GetImage()
  {
    return await _context.Image.ToListAsync();
  }
  [HttpGet("test")]
  public string Test()
  {
    return "test";
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Image>> GetImage(int id)
  {
    var image = await _context.Image.FindAsync(id);

    if (image == null)
    {
      return NotFound();
    }

    return image;
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> PutImage(int id, Image image)
  {
    if (id != image.Id)
    {
      return BadRequest();
    }

    _context.Entry(image).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!ImageExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    return NoContent();
  }

  [HttpPost]
  public async Task<ActionResult<Image>> PostImage(Image image)
  {
    _context.Image.Add(image);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetImage", new { id = image.Id }, image);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteImage(int id)
  {
    var image = await _context.Image.FindAsync(id);
    if (image == null)
    {
      return NotFound();
    }

    _context.Image.Remove(image);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool ImageExists(int id)
  {
    return _context.Image.Any(e => e.Id == id);
  }
}

