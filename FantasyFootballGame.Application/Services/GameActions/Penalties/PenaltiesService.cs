using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves;
using FantasyFootballGame.Application.Interfaces.GameActions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.Penalties;
using FantasyFootballGame.Domain.Models.Actions.Penalties;

namespace FantasyFootballGame.Application.Services.GameActions.Penalties
{
    public class PenaltiesService : IPenaltiesService
    {
        private readonly IPenaltiesRepository _repo;
        private readonly IMapper _mapper;
        private readonly IPenaltiesMissedService _penaltiesMissedService;
        private readonly IPenaltiesSavesService _penaltiesSavesService;
        public PenaltiesService(
            IPenaltiesRepository penaltiesRepository,
            IPenaltiesMissedService penaltiesMissedService,
            IPenaltiesSavesService penaltiesSavesService,
            IMapper mapper)
        {
            _mapper = mapper;
            _repo = penaltiesRepository;
            _penaltiesMissedService = penaltiesMissedService;
            _penaltiesSavesService = penaltiesSavesService;
        }


        public async Task Delete(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty not found");
            _repo.Delete(penalty);
            await _repo.Save();
        }

        public async Task<PenaltyResponseDto> GetById(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty not found");
            return _mapper.Map<PenaltyResponseDto>(penalty);
        }

        public async Task<PenaltyResponseDto> Create(CreatePenaltyDto dto)
        {
            var penalty = _mapper.Map<Penalty>(dto);
            await _repo.Create(penalty);
            await _repo.Save();
            if(!penalty.IsScored)
            {
                if (dto.MissPlayerId.HasValue) 
                {
                    var missDto = new CreatePenaltyMissedDto
                    {
                        PenaltyId = penalty.Id,
                        PlayerId = dto.MissPlayerId.Value,
                    };
                    await _penaltiesMissedService.Create(missDto);
                }
                if (dto.SavePlayerId.HasValue)
                {
                    var saveDto = new CreatePenaltySaveDto
                    {
                        PenaltyId = penalty.Id,
                        PlayerId = dto.SavePlayerId.Value,
                    };
                    await _penaltiesSavesService.Create(saveDto);
                }
            }
            return _mapper.Map<PenaltyResponseDto>(penalty);
        }


        public async Task<PenaltyResponseDto> Update(int id, UpdatePenaltyDto dto)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty not found");
            var updatedPenalty = _mapper.Map<Penalty>(penalty);
            _repo.Update(updatedPenalty);
            await _repo.Save();
            return _mapper.Map<PenaltyResponseDto>(updatedPenalty);
        }
    }
}
