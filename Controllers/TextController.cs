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
    public async Task<ActionResult<TextPost>> PostTextPost(TextPost tp)
    {
      var textPost = new TextPost { Name = tp.Name, Text = tp.Text};

      _context.TextPost.Add(textPost);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetTextPost", new { id = textPost.Id }, textPost);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTextPost(int id)
    {
      var textPost = await _context.TextPost.FindAsync(id);
      if (textPost == null) return NotFound();
    
      _context.TextPost.Remove(textPost);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool TextPostExists(int id)
    {
      return _context.TextPost.Any(e => e.Id == id);
    }

    [HttpGet("temporary")]
    public IEnumerable<TextPost> Temporary()
    {
      return new List<TextPost> 
      { 
        new TextPost { Id = 1, Name = "home", Text = "Check my About me and Projects pages for more info." }, 
        new TextPost { Id = 3, Name = "about", Text = "I have worn many hats in my career, I have been a Bus Driver, a Security Guard. I have worked with alarm installations, on and off I’ve been in sales, in total I estimate it to amount to six years or so.\nI worked for several years in traffic control management ensuring the delivery of public bus transportation in stockholm.\nI managed to build and run my own Café in 2010-2011 and as I proved competent in sales I was promoted to storemanager and ran my own store when I worked for GameStop.\n\nAs these jobs ate my twenties away and I found myself making lateral movements over and over in my career I finally came to the belated realisation that some education and time spent on self improvement would probably break the mould and lead me to a more progressive life.\n\nWhen in my late teens / early twenties I completed my education at Komvux, I had a short fling with web development and enjoyed it immensely.\nIt combined my love for puzzles with the esthetical and creative part of me in a hugely satisfactory way. I completed the course with lots of praise from my instructors but as I had no clear road of advancement in the field laid before me I let it lie, until in my early thirties did some belated soul searching and over all life evaluation.\n\nI remembered my love for coding and development and decided that this will be my goal, I will become a software developer.\n\nAfter some attempts to find a good learning track on my own I stumbled upon </salt>.\nI applied, got in, and gave it 110%. I graduated as a Full-Stack .Net Developer and it resulted in me being hired on as an Instructor for the subsequent two following bootcamps.\nIt was chaotic, it was challenging and it was one of the most rewarding experiences of my life." } 
      };
    }
  }

