using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

  namespace portfolio_backend;
  [ApiController]
  [Route("api/[controller]")]
  public class TextsController : ControllerBase
  {
    private readonly TextDbContext _context;

    public TextsController(TextDbContext context) => _context = context;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TextPost>>> GetTextPost()
      => await _context.TextPost.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<TextPost>> GetTextPost(int id)
    {
      var textPost = await _context.TextPost.FindAsync(id);

      return (textPost == null) ? NotFound() : textPost;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTextPost(int id, TextPost textPost)
    {
      if (id != textPost.Id) return BadRequest();

      _context.Entry(textPost).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        return (!TextPostExists(id)) 
          ? NotFound() 
          : throw new Exception("Error updating text post");
      }

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<TextPost>> PostTextPost(TextPostDTO textPostDTO)
    {
      var textPost = new TextPost { Name = textPostDTO.Name, Text = textPostDTO.Text};

      _context.TextPost.Add(textPost);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetTextPost", new { id = textPost.Id }, textPost);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTextPost(int id)
    {
      var textPost = await _context.TextPost.FindAsync(id);
      if (textPost == null)
      {
        return NotFound();
      }

      _context.TextPost.Remove(textPost);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool TextPostExists(int id)
    {
      return _context.TextPost.Any(e => e.Id == id);
    }

    [HttpGet("test")]
    public IEnumerable<TextPost> Test()
    {
      return new List<TextPost> 
      { 
        new TextPost { Id = 1, Name = "Why yes", Text = "yes we can" }, 
        new TextPost { Id = 2, Name = "This is currently deployed", Text = "on Azure" } 
      };
    }
  }

