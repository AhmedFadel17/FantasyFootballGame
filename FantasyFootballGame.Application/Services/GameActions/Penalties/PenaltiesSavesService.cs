using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves;
using FantasyFootballGame.Application.Interfaces.GameActions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesSaves;
using FantasyFootballGame.Domain.Models.Actions.Penalties;

namespace FantasyFootballGame.Application.Services.GameActions.Penalties
{
    public class PenaltiesSavesService : IPenaltiesSavesService
    {
        private readonly IPenaltiesSavesRepository _repo;
        private readonly IMapper _mapper;

        public PenaltiesSavesService(
            IPenaltiesSavesRepository penaltiesSavesRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repo = penaltiesSavesRepository;
        }

        public async Task<PenaltySaveResponseDto> Create(CreatePenaltySaveDto dto)
        {
            var penalty = _mapper.Map<PenaltySave>(dto);
            await _repo.Create(penalty);
            await _repo.Save();
            return _mapper.Map<PenaltySaveResponseDto>(penalty);
        }

        public async Task Delete(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty save not found");
            _repo.Delete(penalty);
            await _repo.Save();
        }

        public async Task<PenaltySaveResponseDto> GetById(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty save not found");
            return _mapper.Map<PenaltySaveResponseDto>(penalty);
        }

        public async Task<PenaltySaveResponseDto> Update(int id, UpdatePenaltySaveDto dto)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty save not found");
            var updatedPenalty = _mapper.Map<PenaltySave>(penalty);
            _repo.Update(updatedPenalty);
            await _repo.Save();
            return _mapper.Map<PenaltySaveResponseDto>(updatedPenalty);
        }
    }
}
