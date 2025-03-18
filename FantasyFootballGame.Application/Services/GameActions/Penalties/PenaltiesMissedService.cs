using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;
using FantasyFootballGame.Application.Interfaces.GameActions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesMissed;
using FantasyFootballGame.Domain.Models.Actions.Penalties;


namespace FantasyFootballGame.Application.Services.GameActions.Penalties
{
    public class PenaltiesMissedService : IPenaltiesMissedService
    {
        private readonly IPenaltiesMissedRepository _repo;
        private readonly IMapper _mapper;

        public PenaltiesMissedService(
            IPenaltiesMissedRepository penaltiesMissedRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repo = penaltiesMissedRepository;
        }

        public async Task<PenaltyMissedResponseDto> Create(CreatePenaltyMissedDto dto)
        {
            var penalty = _mapper.Map<PenaltyMiss>(dto);
            await _repo.Create(penalty);
            await _repo.Save();
            return _mapper.Map<PenaltyMissedResponseDto>(penalty);
        }

        public async Task Delete(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty miss not found");
            _repo.Delete(penalty);
            await _repo.Save();
        }

        public async Task<PenaltyMissedResponseDto> GetById(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty miss not found");
            return _mapper.Map<PenaltyMissedResponseDto>(penalty);
        }

        public async Task<PenaltyMissedResponseDto> Update(int id, UpdatePenaltyMissedDto dto)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty miss not found");
            var updatedPenalty = _mapper.Map<PenaltyMiss>(penalty);
            _repo.Update(updatedPenalty);
            await _repo.Save();
            return _mapper.Map<PenaltyMissedResponseDto>(updatedPenalty);
        }
    }
}
