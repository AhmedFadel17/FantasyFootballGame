
namespace FantasyFootballGame.Domain.Models
{
    public record FantasyTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public double SquadValue { get; set; }
        public double InTheBank { get; set; }
        public int UserId { get; set; }
    }
}
