using FantasyFootballGame.Application.DTOs.Transfers;

namespace FantasyFootballGame.Application.DTOs.FantasyTeams
{
    public record MakeTransfersDto
    {
        public int FantasyTeamId { get; set; }
        public int GameweekId { get; set; }
        List<CreateTransferDto> transfers {  get; set; }
    }
}
