using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gameweek>()
                .HasIndex(g => g.WeekNumber)
                .IsUnique();

            modelBuilder.Entity<FantasyTeam>()
                .HasIndex(g => g.Name)
                .IsUnique();
        }
    }
}
