using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
