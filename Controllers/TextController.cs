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
    var textPost = new TextPost { Name = tp.Name, Text = tp.Text };

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
        new TextPost { Id = 3, Name = "about", Text = "I have worn many hats throughout my career. Initially, I drove buses for nine years before being drafted into management, where I worked in traffic control, ensuring the delivery of public bus transportation in Stockholm for several years.\n\nFor a brief period, I worked in security, preceded by an equally brief apprenticeship in alarm installations. On and off, I have been involved in sales, accumulating over six years of experience in this field. Thanks to consistently high KPIs, I was promoted to store manager and successfully ran my own GameStop store.\nAdditionally, I managed to build and run my own café between 2010 and 2011. Managing my own business was both challenging and rewarding.\n\nThese jobs consumed my twenties, and I found myself making lateral moves repeatedly without advancing in any notable career. Eventually, I realized the importance of education and self-improvement.\n\nIn my late teens, I completed my education at Komvux, where one of the courses I took was web development, which I immensely enjoyed. It combined my love for puzzles with creativity in a highly satisfactory manner. Despite receiving lots of praise from my instructors, there was no clear path forward, leading to a period of stagnation until a life re-evaluation in my early thirties.\n\nThe idea of coding and development resurfaced, and I decided to pursue it. After attempting to find a suitable learning path on my own, I discovered </salt> and their bootcamp approach to training up-and-coming developers. I applied, was accepted, and committed myself fully. In March 2022, I began the pre-course material, and by August of the same year, I emerged as a Full-Stack .Net Developer, feeling a sense of real accomplishment for the first time. However, my journey didn't stop there.\n\nIn the last week of the bootcamp, I was offered a position to join the instructors' guild at </salt>. I accepted the offer and served as a junior instructor for the next two bootcamps.\n\nMost recently, I spent nine months as one of </salt>'s consultants, placed with Dormakaba Scanbalt, where I developed integration solutions between their Locks and Doors API and customers' personnel applications." }
      };
  }
}

