using Microsoft.EntityFrameworkCore;

namespace portfolio_backend;
public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext (DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Image { get; set; } = default!;
        public DbSet<TextPost> TextPost { get; set; } = default!;
    }
