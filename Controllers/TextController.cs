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
        new TextPost { Id = 3, Name = "about", Text = "I have worn many hats in my career, When I had driven buses for nine years I was drafted to management and worked for several years in traffic control ensuring the delivery of public bus transportation in stockholm.\nFor a brief time I worked in Security and before that there was an equally brief apprenticeship in alarm installations.\n On and off I’ve been in sales and in total I estimate it to amount to six plus years of sales experience, as my KPI's were consistantly high I was promoted to store-manager and ran my own GameStop store.\nAs an aside, I managed to build and run my own Café in 2010-2011, managing your own business was both challenging and rewarding.\n\nThese jobs ate my twenties away and I found myself making lateral movements over and over without furthering any notable career to speak of.\nOne day the belated realisation came that some education and time spent on self improvement was over due.\n\nWhen in my late teens I completed my education at Komvux, one of the courses I took was web development and I enjoyed it immensely.\nIt combined my love for puzzles with creativity in a hugely satisfactory manner. After completing the course with lots of praise from my instructors there was no clear road of progression ahead and thus lead to nothing further, until in my early thirties did the aforementioned life re-evaluation.\n\nCoding and development sprung to mind again and a decision was made.\nAfter some attempts to find a good learning track on my own I stumbled upon </salt> and their bootcamp approach to instructing up and coming developers.\nI applied, got in, and gave it 110%, march 2022 I dove into the precourse material and august the same year a Full-Stack .Net Developer hatched and it resulted in the feeling of real accomplishment for the first time, it wouldn't stop there however\n\nMy last week in the bootcamp I was offered to join the instructors guild at </salt>, I ofcourse accepted and took up the mantle of junior Instructor for the subsequent two following bootcamps.\n\nMost recently I spent nine months as one of </salt>'s consultants placed with Dormakaba Scanbalt where I developed integration solutions between their Locks and Doors API and customers personell applications." }
      };
  }
}

