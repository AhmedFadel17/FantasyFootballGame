using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Application.DTOs.GameActions.Cards;

namespace FantasyFootballGame.Application.Interfaces.GameActions.Cards
{
    public interface ICardsService
    {
        Task<CardResponseDto> GetById(int id);
        Task<PaginationDto<CardResponseDto>> GetAllWithPagination(
            int page,
            int pageSize,
            int? playerId,
            int? teamId,
            int? fixtureId,
            int? gameweekId
            );
        Task<CardResponseDto> Update(int id, UpdateCardDto dto);
        Task<CardResponseDto> Create(CreateCardDto dto);
        Task Delete(int id);
    }
}
