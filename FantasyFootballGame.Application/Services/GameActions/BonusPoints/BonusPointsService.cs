using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints;
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints;
using FantasyFootballGame.Application.Interfaces.GameActions.BonusPoints;
using FantasyFootballGame.DataAccess.Repositories.Actions.BonusPoints;
using FantasyFootballGame.Domain.Models.Actions;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameActions.BonusPoints
{
    public class BonusPointsService : IBonusPointsService
    {
        private readonly IBonusPointsRepository _bonusPointsRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBonusPointsDto> _createValidator;
        private readonly IValidator<UpdateBonusPointsDto> _updateValidator;

        public BonusPointsService(
            IBonusPointsRepository bonusPointsRepo,
            IMapper mapper,
            IValidator<CreateBonusPointsDto> createValidator,
            IValidator<UpdateBonusPointsDto> updateValidator)
        {
            _bonusPointsRepo = bonusPointsRepo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<BonusPointsResponseDto> Create(CreateBonusPointsDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var bonusPoints = _mapper.Map<Bonus>(dto);
            await _bonusPointsRepo.Create(bonusPoints);
            await _bonusPointsRepo.Save();
            return _mapper.Map<BonusPointsResponseDto>(bonusPoints);
        }

        public async Task Delete(int id)
        {
            var bonusPoints = await _bonusPointsRepo.GetById(id);
            if (bonusPoints == null)
                throw new Exception($"Bonus points with id {id} not found");
            _bonusPointsRepo.Delete(bonusPoints);
            await _bonusPointsRepo.Save();
        }

        public async Task<List<BonusPointsResponseDto>> GetByFixture(int fixtureId)
        {
            var bonusPoints = await _bonusPointsRepo.GetByFixture(fixtureId);
            return _mapper.Map<List<BonusPointsResponseDto>>(bonusPoints);
        }

        public async Task<BonusPointsResponseDto> GetById(int id)
        {
            var bonusPoints = await _bonusPointsRepo.GetById(id);
            if (bonusPoints == null)
                throw new Exception($"Bonus points with id {id} not found");
            return _mapper.Map<BonusPointsResponseDto>(bonusPoints);
        }

        public async Task<BonusPointsResponseDto> Update(int id, UpdateBonusPointsDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var bonusPoints = await _bonusPointsRepo.GetById(id);
            if (bonusPoints == null)
                throw new Exception($"Bonus points with id {id} not found");
            _mapper.Map(dto, bonusPoints);
            _bonusPointsRepo.Update(bonusPoints);
            await _bonusPointsRepo.Save();
            return _mapper.Map<BonusPointsResponseDto>(bonusPoints);
        }
    }
}
