using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.Domain.Models.Actions;
using FantasyFootballGame.Domain.Models.Actions.Goals;
using FantasyFootballGame.Domain.Models.Actions.Penalties;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public readonly DbSet<Team> Teams;
        public readonly DbSet<Player> Players;
        public readonly DbSet<Gameweek> Gameweeks;
        public readonly DbSet<FantasyTeam> FantasyTeams;
        public readonly DbSet<FantasyTeamPlayer> FantasyPlayers;
        public readonly DbSet<GameweekTeam> GameweekTeams;
        public readonly DbSet<GameweekTeamPlayer> GameweekPlayers;
        public readonly DbSet<Fixture> Fixtures;
        public readonly DbSet<PlayerStat> PlayerStats;
        public readonly DbSet<Transfer> Transfers;
        public readonly DbSet<Goal> Goals;
        public readonly DbSet<GoalScored> GoalsScored;
        public readonly DbSet<OwnGoal> OwnGoals;
        public readonly DbSet<Assist> assists;
        public readonly DbSet<Penalty> Penalties;
        public readonly DbSet<PenaltyMiss> PenaltiesMiss;
        public readonly DbSet<PenaltySave> PenaltiesSave;
        public readonly DbSet<Injury> Injuries;
        public readonly DbSet<Save> Saves;
        public readonly DbSet<Card> Cards;
        public readonly DbSet<Bonus> Bonus;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
