using Microsoft.EntityFrameworkCore;

namespace portfolio_backend;
public class TextDbContext : DbContext
{
    public TextDbContext(DbContextOptions<TextDbContext> options) : base(options)
    {
    }
    public DbSet<TextPost> TextPost => Set<TextPost>();
   
}
