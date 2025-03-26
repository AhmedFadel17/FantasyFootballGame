using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Cards;
using FantasyFootballGame.Application.Interfaces.GameActions.Cards;
using FantasyFootballGame.DataAccess.Repositories.Actions.RedCards;
using FantasyFootballGame.Domain.Models.Actions;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameActions.Cards
{
    public class CardsService : ICardsService
    {
        private readonly ICardsRepository _cardsRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCardDto> _createValidator;
        private readonly IValidator<UpdateCardDto> _updateValidator;

        public CardsService(
            ICardsRepository cardsRepo,
            IMapper mapper,
            IValidator<CreateCardDto> createValidator,
            IValidator<UpdateCardDto> updateValidator)
        {
            _cardsRepo = cardsRepo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<CardResponseDto> Create(CreateCardDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var card = _mapper.Map<Card>(dto);
            await _cardsRepo.Create(card);
            await _cardsRepo.Save();
            return _mapper.Map<CardResponseDto>(card);
        }

        public async Task Delete(int id)
        {
            var card = await _cardsRepo.GetById(id);
            if (card == null)
                throw new Exception($"Card with id {id} not found");
            _cardsRepo.Delete(card);
            await _cardsRepo.Save();
        }

        public async Task<List<CardResponseDto>> GetByFixture(int fixtureId)
        {
            var cards = await _cardsRepo.GetByFixture(fixtureId);
            return _mapper.Map<List<CardResponseDto>>(cards);
        }

        public async Task<List<CardResponseDto>> GetByGameweek(int gameweekId)
        {
            var cards = await _cardsRepo.GetByGameweek(gameweekId);
            return _mapper.Map<List<CardResponseDto>>(cards);
        }

        public async Task<CardResponseDto> GetById(int id)
        {
            var card = await _cardsRepo.GetById(id);
            if (card == null)
                throw new Exception($"Card with id {id} not found");
            return _mapper.Map<CardResponseDto>(card);
        }

        public async Task<List<CardResponseDto>> GetByPlayer(int playerId)
        {
            var cards = await _cardsRepo.GetByPlayer(playerId);
            return _mapper.Map<List<CardResponseDto>>(cards);
        }

        public async Task<List<CardResponseDto>> GetByTeam(int teamId)
        {
            var cards = await _cardsRepo.GetByTeam(teamId);
            return _mapper.Map<List<CardResponseDto>>(cards);
        }

        public async Task<CardResponseDto> Update(int id, UpdateCardDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var card = await _cardsRepo.GetById(id);
            if (card == null)
                throw new Exception($"Card with id {id} not found");
            _mapper.Map(dto, card);
            _cardsRepo.Update(card);
            await _cardsRepo.Save();
            return _mapper.Map<CardResponseDto>(card);
        }
    }
}
