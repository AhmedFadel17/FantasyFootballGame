using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Cards;
using FantasyFootballGame.Application.Interfaces.GameActions.Cards;
using FantasyFootballGame.DataAccess.Repositories.Actions.RedCards;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.Application.Services.GameActions.Cards
{
    public class CardsService : ICardsService
    {
        private readonly ICardsRepository _repo;
        private readonly IMapper _mapper;
        public CardsService(ICardsRepository cardsRepository,IMapper mapper)
        {
            _mapper = mapper;
            _repo = cardsRepository;
        }
        public async Task<CardResponseDto> Create(CreateCardDto dto)
        {
            var card = _mapper.Map<Card>(dto);
            await _repo.Create(card);
            await _repo.Save();
            return _mapper.Map<CardResponseDto>(card);
        }

        public async Task Delete(int id)
        {
            var card= await _repo.GetById(id);
            if (card == null) throw new KeyNotFoundException("Card not found");
            _repo.Delete(card);
            await _repo.Save();
        }

        public async Task<CardResponseDto> GetById(int id)
        {
            var card = await _repo.GetById(id);
            if (card == null) throw new KeyNotFoundException("Card not found");
            return _mapper.Map<CardResponseDto>(card);
        }

        public async Task<CardResponseDto> Update(int id, UpdateCardDto dto)
        {
            var card = await _repo.GetById(id);
            if (card == null) throw new KeyNotFoundException("Card not found");
            var updatedCard= _mapper.Map<Card>(card);
            _repo.Update(updatedCard);
            await _repo.Save();
            return _mapper.Map<CardResponseDto>(updatedCard);
        }
    }
}
