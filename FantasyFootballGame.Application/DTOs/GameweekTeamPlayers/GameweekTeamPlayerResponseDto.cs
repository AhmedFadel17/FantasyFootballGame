using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.DTOs.GameweekTeamPlayers
{
    public record GameweekTeamPlayerResponseDto
    {
        public int Id { get; set; }
        public int GameweekTeamId { get; set; }
        public int PlayerId { get; set; }
        public int FantasyTeamPlayerId { get; set; }
        public bool IsStarting { get; set; }
        public int PosNum { get; set; }
        public bool IsCaptain { get; set; }
        public bool IsViceCaptain { get; set; }
        public int Points { get; set; }
        public PlayerResponseDto? Player { get; set; }
        public FixtureResponseDto? UpcomingFixture { get; set; }
    }
}
