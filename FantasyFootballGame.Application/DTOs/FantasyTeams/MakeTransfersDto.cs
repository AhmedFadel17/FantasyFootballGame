using FantasyFootballGame.Application.DTOs.Transfers;

namespace FantasyFootballGame.Application.DTOs.FantasyTeams
{
    public record MakeTransfersDto
    {
        public required int FantasyTeamId { get; set; }
        public required List<CreateTransferDto> transfers {  get; set; }
    }
}
