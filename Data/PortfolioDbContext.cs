using Microsoft.EntityFrameworkCore;

namespace portfolio_backend;
public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext (DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
        }
        public DbSet<TextPost> TextPost { get; set; } = default!;
    }
