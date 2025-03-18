using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints;
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints;
using FantasyFootballGame.Application.Interfaces.GameActions.BonusPoints;
using FantasyFootballGame.DataAccess.Repositories.Actions.BonusPoints;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.Application.Services.GameActions.BonusPoints
{
    public class BonusPointsService : IBonusPointsService
    {
        private readonly IBonusPointsRepository _repo;
        private readonly IMapper _mapper;
        public BonusPointsService(IBonusPointsRepository bonusPointsRepository,IMapper mapper)
        {
            _mapper = mapper;
            _repo = bonusPointsRepository;
        }

        public async Task<BonusPointsResponseDto> Create(CreateBonusPointsDto dto)
        {
            var bonusPoints = new List<Bonus>();

            foreach (var item in dto.BonusPoints)
            {
                var bonus = _mapper.Map<Bonus>(item);
                await _repo.Create(bonus);
                bonusPoints.Add(bonus);
            }

            await _repo.Save();

            var response = new BonusPointsResponseDto
            {
                FixtureId = dto.FixtureId,
                BonusPoints = bonusPoints.Select(b => _mapper.Map<BonusPointResponseDto>(b)).ToList()
            };

            return response;
        }


        public async Task Delete(int id)
        {
            var bonus = await _repo.GetById(id);
            if (bonus == null) throw new KeyNotFoundException("Bonus not found");
            _repo.Delete(bonus);
            await _repo.Save();
        }

        public async Task<BonusPointsResponseDto> GetById(int id)
        {
            var bonus = await _repo.GetById(id);
            if (bonus == null) throw new KeyNotFoundException("Bonus not found");
            return _mapper.Map<BonusPointsResponseDto>(bonus);
        }

        public async Task<BonusPointsResponseDto> Update(int id, UpdateBonusPointsDto dto)
        {
            var bonusPointsList = new List<Bonus>();

            var bonusPoints = await _repo.GetByFixture(id);
            var newBonusPoints = dto.BonusPoints;
            foreach (var item in bonusPoints)
            {
                _repo.Delete(item);
            }
            foreach (var item in newBonusPoints)
            {
                var b = _mapper.Map<Bonus>(item);
                await _repo.Create(b);
                bonusPointsList.Add(b);
            }
            await _repo.Save();
            var response = new BonusPointsResponseDto
            {
                FixtureId = id,
                BonusPoints = bonusPoints.Select(b => _mapper.Map<BonusPointResponseDto>(b)).ToList()
            };

            return response;
        }
    }
}
