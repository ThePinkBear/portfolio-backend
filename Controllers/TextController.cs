using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace portfolio_backend
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly PortfolioDbContext _context;

        public TextController(PortfolioDbContext context)
        {
            _context = context;
        }

        // GET: api/Text
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TextPost>>> GetTextPost()
        {
            return await _context.TextPost.ToListAsync();
        }
        [HttpGet("test")]
        public string Test()
        {
            return "test";
        }

        // GET: api/Text/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TextPost>> GetTextPost(int id)
        {
            var textPost = await _context.TextPost.FindAsync(id);

            if (textPost == null)
            {
                return NotFound();
            }

            return textPost;
        }

        // PUT: api/Text/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTextPost(int id, TextPost textPost)
        {
            if (id != textPost.Id)
            {
                return BadRequest();
            }

            _context.Entry(textPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextPostExists(id))
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

        // POST: api/Text
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TextPost>> PostTextPost(TextPost textPost)
        {
            _context.TextPost.Add(textPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTextPost", new { id = textPost.Id }, textPost);
        }

        // DELETE: api/Text/5
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
    }
}
