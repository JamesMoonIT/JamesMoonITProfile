using JamesMoonPortfolioRedux.Models;
using Microsoft.EntityFrameworkCore;

namespace JamesMoonPortfolioRedux.Data
{
    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options)
        { 
        }

        public DbSet<Comment> Comment { get; set; }
    }
}
