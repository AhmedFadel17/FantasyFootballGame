using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints;
using FantasyFootballGame.Application.DTOs.GameActions.Cards;

namespace FantasyFootballGame.Application.Interfaces.GameActions.BonusPoints
{
    public interface IBonusPointsService
    {
        Task<BonusPointsResponseDto> GetById(int id);
        //Task<List<BonusPointsResponseDto>> GetByFixture(int fixtureId);
        Task<BonusPointsResponseDto> Update(int id, UpdateBonusPointsDto dto);
        Task<BonusPointsResponseDto> Create(CreateBonusPointsDto dto);
        Task Delete(int id);
    }
}
