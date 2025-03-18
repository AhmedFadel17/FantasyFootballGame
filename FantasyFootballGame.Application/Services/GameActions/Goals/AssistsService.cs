using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Goals.Assists;
using FantasyFootballGame.Application.Interfaces.GameActions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Actions.Assists;
using FantasyFootballGame.Domain.Models.Actions.Goals;

namespace FantasyFootballGame.Application.Services.GameActions.Goals
{
    public class AssistsService : IAssistsService
    {
        private readonly IAssistsRepository _repo;
        private readonly IMapper _mapper;
        public AssistsService(IAssistsRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repository;
        }
        public async Task<AssistResponseDto> Create(CreateAssistDto dto)
        {
            bool goalHasAssits = await _repo.CheckGoalHasAssist(dto.GoalId);
            if (goalHasAssits) throw new InvalidOperationException("This Goal has already assist");
            var assist = _mapper.Map<Assist>(dto);
            await _repo.Create(assist);
            await _repo.Save();
            return _mapper.Map<AssistResponseDto>(assist);
        }

        public async Task Delete(int id)
        {
            var assist = await _repo.GetById(id);
            if (assist == null) throw new KeyNotFoundException("Assist not found");
            _repo.Delete(assist);
            await _repo.Save();
        }

        public async Task<AssistResponseDto> GetById(int id)
        {
            var assist = await _repo.GetById(id);
            if (assist == null) throw new KeyNotFoundException("Assist not found");
            return _mapper.Map<AssistResponseDto>(assist);
        }

        public async Task<AssistResponseDto> Update(int id, UpdateAssistDto dto)
        {
            var assist = await _repo.GetById(id);
            if (assist == null) throw new KeyNotFoundException("Assist not found");
            var updatedAssist = _mapper.Map<Assist>(dto);
            _repo.Update(updatedAssist);
            await _repo.Save();
            return _mapper.Map<AssistResponseDto>(updatedAssist);
        }
    }
}
