using FantasyFootballGame.Application.DTOs.GameActions.Cards;

namespace FantasyFootballGame.Application.Interfaces.GameActions.Cards
{
    public interface ICardsService
    {
        Task<CardResponseDto> GetById(int id);
        Task<List<CardResponseDto>> GetByFixture(int fixtureId);
        Task<List<CardResponseDto>> GetByGameweek(int gameweekId);
        Task<List<CardResponseDto>> GetByPlayer(int playerId);
        Task<List<CardResponseDto>> GetByTeam(int teamId);
        Task<CardResponseDto> Update(int id, UpdateCardDto dto);
        Task<CardResponseDto> Create(CreateCardDto dto);
        Task Delete(int id);
    }
}
