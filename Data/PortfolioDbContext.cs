using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext (DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Image { get; set; } = default!;

        public DbSet<TextPost> TextPost { get; set; } = default!;
    }
