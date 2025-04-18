using AutoMapper;
using FantasyFootballGame.Application.DTOs.Common;
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

        public async Task<PaginationDto<CardResponseDto>> GetAllWithPagination(int page, int pageSize, int? playerId, int? teamId, int? fixtureId, int? gameweekId)
        {
            var cards =  await _cardsRepo.GetAllWithPagination(page, pageSize, playerId, teamId, fixtureId, gameweekId);
            var paginationSource = new PaginationSource<Card>(cards.Item1.ToList(), page, pageSize, cards.Item2);
            return _mapper.Map<PaginationDto<CardResponseDto>>(paginationSource);
        }

        

        public async Task<CardResponseDto> GetById(int id)
        {
            var card = await _cardsRepo.GetById(id);
            if (card == null)
                throw new Exception($"Card with id {id} not found");
            return _mapper.Map<CardResponseDto>(card);
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
