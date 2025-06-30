using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.Domain.Models.Actions;
using FantasyFootballGame.Domain.Models.Actions.Goals;
using FantasyFootballGame.Domain.Models.Actions.Penalties;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Gameweek> Gameweeks { get; set; }
        public DbSet<FantasyTeam> FantasyTeams { get; set; }
        public DbSet<FantasyTeamPlayer> FantasyPlayers { get; set; }
        public DbSet<GameweekTeam> GameweekTeams { get; set; }
        public DbSet<GameweekTeamPlayer> GameweekPlayers { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<PlayerStat> PlayerStats { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalScored> GoalsScored { get; set; }
        public DbSet<OwnGoal> OwnGoals { get; set; }
        public DbSet<Assist> Assists { get; set; }         // Also renamed to PascalCase
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<PenaltyMiss> PenaltiesMiss { get; set; }
        public DbSet<PenaltySave> PenaltiesSave { get; set; }
        public DbSet<Injury> Injuries { get; set; }
        public DbSet<Save> Saves { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Bonus> Bonus { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameweekTeamPlayer>()
    .HasOne(p => p.FantasyTeamPlayer)
    .WithMany()
    .HasForeignKey(p => p.FantasyTeamPlayerId)
    .OnDelete(DeleteBehavior.Restrict); // or .NoAction()

            modelBuilder.Entity<GameweekTeamPlayer>()
                .HasOne(p => p.Player)
                .WithMany()
                .HasForeignKey(p => p.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameweekTeamPlayer>()
                .HasOne(p => p.GameweekTeam)
                .WithMany()
                .HasForeignKey(p => p.GameweekTeamId)
                .OnDelete(DeleteBehavior.Cascade); // one allowed cascade

            modelBuilder.Entity<Transfer>()
    .HasOne(t => t.PlayerIn)
    .WithMany()
    .HasForeignKey(t => t.PlayerInId)
    .OnDelete(DeleteBehavior.Restrict); // or .NoAction()

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.PlayerOut)
                .WithMany()
                .HasForeignKey(t => t.PlayerOutId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Save>()
.HasOne(c => c.Fixture)
.WithMany()
.HasForeignKey(c => c.FixtureId)
.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Save>()
                .HasOne(c => c.Player)
                .WithMany()
                .HasForeignKey(c => c.PlayerId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction()

            modelBuilder.Entity<Save>()
                .HasOne(c => c.Team)
                .WithMany()
                .HasForeignKey(c => c.TeamId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction()
            modelBuilder.Entity<Card>()
    .HasOne(c => c.Fixture)
    .WithMany()
    .HasForeignKey(c => c.FixtureId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.Player)
                .WithMany()
                .HasForeignKey(c => c.PlayerId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction()

            modelBuilder.Entity<Card>()
                .HasOne(c => c.Team)
                .WithMany()
                .HasForeignKey(c => c.TeamId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction()
            modelBuilder.Entity<Fixture>()
    .HasOne(f => f.HomeTeam)
    .WithMany()
    .HasForeignKey(f => f.HomeTeamId)
    .OnDelete(DeleteBehavior.Restrict); // or NoAction
            modelBuilder.Entity<FantasyTeam>()
                .HasIndex(g => g.UserId)
                .IsUnique();
            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.AwayTeam)
                .WithMany()
                .HasForeignKey(f => f.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict); // or NoAction

            modelBuilder.Entity<PenaltyMiss>()
    .HasOne(pm => pm.Player)
    .WithMany()
    .HasForeignKey(pm => pm.PlayerId)
    .OnDelete(DeleteBehavior.Restrict); // prevent cascade here

            modelBuilder.Entity<PenaltyMiss>()
                .HasOne(pm => pm.Penalty)
                .WithMany()
                .HasForeignKey(pm => pm.PenaltyId)
                .OnDelete(DeleteBehavior.Cascade); // keep this one

            modelBuilder.Entity<PenaltySave>()
.HasOne(pm => pm.Player)
.WithMany()
.HasForeignKey(pm => pm.PlayerId)
.OnDelete(DeleteBehavior.Restrict); // prevent cascade here

            modelBuilder.Entity<PenaltySave>()
                .HasOne(pm => pm.Penalty)
                .WithMany()
                .HasForeignKey(pm => pm.PenaltyId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Gameweek>()
                .HasIndex(g => g.WeekNumber)
                .IsUnique();

            modelBuilder.Entity<FantasyTeam>()
                .HasIndex(g => g.Name)
                .IsUnique();

            modelBuilder.Entity<PenaltyMiss>()
                .HasIndex(g => g.PenaltyId)
                .IsUnique();

            modelBuilder.Entity<PenaltySave>()
                .HasIndex(g => g.PenaltyId)
                .IsUnique();

            modelBuilder.Entity<GoalScored>()
                .HasIndex(g => g.GoalId)
                .IsUnique();

            modelBuilder.Entity<Assist>()
                .HasIndex(g => g.GoalId)
                .IsUnique();

            modelBuilder.Entity<OwnGoal>()
                .HasIndex(g => g.GoalId)
                .IsUnique();

        }
    }
}
